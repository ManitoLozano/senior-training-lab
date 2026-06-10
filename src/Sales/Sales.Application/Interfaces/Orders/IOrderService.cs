using Sales.Application.Models.Orders;

namespace Sales.Application.Interfaces.Orders;

public interface IOrderService
{
    Task<IReadOnlyList<OrderOutput?>> GetAllAsync();
    Task<OrderOutput?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<OrderOutput?>> GetByCustomerIdAsync(Guid customerId);
    Task<OrderOutput?> AddOrderAsync(CreateOrderInput order);
    Task<OrderOutput?> UpdateOrderAsync(Guid id, UpdateOrderInput order);
    Task<OrderOutput?> ConfirmAsync(Guid id);
    Task<OrderOutput?> CancelAsync(Guid id);
    Task<OrderOutput?> SentToFulfillmentAsync(Guid id);
    Task DeleteOrderAsync(Guid id);
}