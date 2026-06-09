using FluentValidation;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Mappers.Orders;
using Sales.Application.Models.Orders;

namespace Sales.Application.Services.Orders;

public class OrderService(
    IOrderRepository orderRepository,
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

    public Task<OrderOutput?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
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

        var newOrder = order.ToEntity();
        newOrder.UpdateStatusToCreate();
        
        await orderRepository.AddAsync(newOrder);
        return newOrder.ToOutput();
    }

    public Task<OrderOutput?> UpdateOrderAsync(UpdateOrderInput order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}