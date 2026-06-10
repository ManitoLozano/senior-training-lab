using FluentValidation;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Interfaces.Products;
using Sales.Application.Mappers.Orders;
using Sales.Application.Models.Orders;
using Sales.Domain.Enums.OrderStatus;
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
            ? throw new KeyNotFoundException("Order not found")
            : order.ToOutput();
    }

    public Task<IReadOnlyList<OrderOutput?>> GetByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderOutput?> AddOrderAsync(CreateOrderInput order)
    {
        var validationResult = await createOrderValidator.ValidateAsync(order);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        
        var customer = await customerRepository.GetByIdAsync(order.CustomerId);
        if (customer is null) throw new KeyNotFoundException("Customer not found");

        var newOrder = new Order(customer.Id);

        foreach (var item in order.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            
            if (product is null) throw new KeyNotFoundException("Product not found");
            if (product.StockQuantity <  item.Quantity) throw new InvalidOperationException("Product stock quantity is greater than the available stock quantity");
            
            newOrder.AddItem(product, item.Quantity);
        }

        newOrder.UpdateStatusToCreate();
        await orderRepository.AddAsync(newOrder);
        
        var createdOrder = await orderRepository.GetByIdAsync(newOrder.Id);
        return createdOrder!.ToOutput();
    }

    public async Task<OrderOutput?> UpdateOrderAsync(Guid id, UpdateOrderInput input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input), "Order cannot be null");
        
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("Order not found");
        
        var customer = await customerRepository.GetByIdAsync(input.CustomerId);
        if (customer is null) throw new KeyNotFoundException("Customer not found");
        
        if (order.Status != OrderStatus.Created) throw new InvalidOperationException("Order needs to be created to edit");
        
        order.UpdateCustomer(customer);
        RemoveDeletedItems(order, input.Items);
        
        await AddOrUpdateItemsAsync(order, input.Items);
        
        await orderRepository.UpdateAsync(order);
        return order.ToOutput();
    }

    public async Task<OrderOutput?> ConfirmAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("Order not found");

        if (order.Status !=  OrderStatus.Created) throw new InvalidOperationException("Order needs to be created to confirm");
        order.UpdateStatusToConfirm();
        
        foreach (var orderItem in order.Items)
        {
            var product = await productRepository.GetByIdAsync(orderItem.ProductId);
            if (product is null) throw new KeyNotFoundException("Product not found");
            
            if (product.StockQuantity < orderItem.Quantity) throw new InvalidOperationException("Product stock quantity is greater than the available stock quantity");
            product.DecreaseStockQuantity(orderItem.Quantity);
        }
        
        await  orderRepository.UpdateAsync(order);
        return order.ToOutput();
    }
    
    public async Task<OrderOutput?> CancelAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("Order not found");
        
        if (order.Status != OrderStatus.Confirmed) throw new InvalidOperationException("Order needs to be confirmed to cancel");
        order.UpdateStatusToCancel();

        foreach (var orderItem in order.Items)
        {
            var product = await productRepository.GetByIdAsync(orderItem.ProductId);
            if (product is null) throw new KeyNotFoundException("Product not found");
            
            product.IncreaseStockQuantity(orderItem.Quantity);
        }
        
        await orderRepository.UpdateAsync(order);
        return order.ToOutput();
    }

    public async Task<OrderOutput?> SentToFulfillmentAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("Order not found");
        
        order.UpdateStatusToSentToFulfillment();
        await orderRepository.UpdateAsync(order);
        
        return order.ToOutput();
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("Order not found");
        
        await orderRepository.DeleteAsync(order);
    }
    
    private async Task AddOrUpdateItemsAsync(Order order, IReadOnlyList<UpdateOrderItemInput> items)
    {
        foreach (var item in items)
        {
            var product =  await productRepository.GetByIdAsync(item.ProductId);
            
            if (product is null) throw new KeyNotFoundException("Product not found");
            
            if (product.StockQuantity < item.Quantity)
                throw new InvalidOperationException("Product stock quantity is greater than the available stock quantity");
            
            var isNewOrderItem = item.Id == Guid.Empty || item.Id is null;

            if (isNewOrderItem)
            {
                order.AddItem(product, item.Quantity);
                continue;
            }
            
            var orderItem = order.Items.FirstOrDefault(i => i.Id == item.Id);
            if (orderItem is null) throw new InvalidOperationException("OrderItem don't exist in this order");
            
            order.UpdateItem(orderItem.Id, product, item.Quantity);
        }
    }

    private static void RemoveDeletedItems(Order order, IReadOnlyList<UpdateOrderItemInput> orderItems)
    {
        var inputOrderItemsIds = orderItems
            .Where(i => i.Id != null && i.Id != Guid.Empty)
            .Select(i => i.Id)
            .ToList();
        
        var orderItemsIdRemoved = order.Items
            .Where(i => !inputOrderItemsIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToList();

        foreach (var itemId in orderItemsIdRemoved)
        {
            order.RemoveItem(itemId);
        }
    }
}