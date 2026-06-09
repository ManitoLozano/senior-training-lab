using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Orders;
using Sales.Domain.Orders;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories.Orders;

public class OrderRepository(SalesDbContext dbContext) : IOrderRepository
{
    public async Task<IReadOnlyList<Order>> GetAllOrdersAsync()
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Include(order => order.Customer)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order?>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}