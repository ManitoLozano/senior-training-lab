using FluentValidation;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Interfaces.Products;
using Sales.Application.Mappers.Orders;
using Sales.Application.Models.Orders;
using Sales.Domain.Orders;

namespace Sales.Application.Services.Orders;

public class OrderService(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    IValidator<CreateOrderInput> createOrderValidator
) : IOrderService
{
    public async Task<IReadOnlyList<OrderOutput?>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllOrdersAsync();

        return orders
            .Select(order => order.ToOutput())
            .ToList();
    }

    public async Task<OrderOutput?> GetByIdAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        
        return order is null
            ? throw new InvalidOperationException("Order not found")
            : order.ToOutput();
    }

    public Task<IReadOnlyList<OrderOutput?>> GetByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderOutput?> AddOrderAsync(CreateOrderInput order)
    {
        var validationResult = await createOrderValidator.ValidateAsync(order);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var customer = await customerRepository.GetByIdAsync(order.CustomerId);
        
        if (customer is null)
            throw new InvalidOperationException("Customer not found");

        var newOrder = new Order(customer.Id);

        foreach (var item in order.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            
            if (product is null)
                throw new InvalidOperationException("Product not found");
            
            if (product.StockQuantity <  item.Quantity)
                throw new InvalidOperationException("Product stock quantity is greater than the available stock quantity");
            
            newOrder.AddItem(product, item.Quantity);
        }

        await orderRepository.AddAsync(newOrder);
        
        var createdOrder = await orderRepository.GetByIdAsync(newOrder.Id);
        return createdOrder!.ToOutput();
    }

    public async Task<OrderOutput?> UpdateOrderAsync(Guid id, UpdateOrderInput input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input), "Order cannot be null");
        var order = await orderRepository.GetByIdAsync(id);
        
        if (order is null) throw new InvalidOperationException("Order not found");
        
        // var productsIds = 
        
        await orderRepository.UpdateAsync(order);
        return order.ToOutput();
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id), "Need id to delete");
        
        await orderRepository.DeleteAsync(id);
    }
}