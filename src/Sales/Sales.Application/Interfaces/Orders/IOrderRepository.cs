using Sales.Domain.Orders;

namespace Sales.Application.Interfaces.Orders;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllOrdersAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Order?>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}