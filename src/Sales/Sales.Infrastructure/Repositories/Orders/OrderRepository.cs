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
            .Include(order => order.Customer)
            .Include(order => order.Items)
            .ThenInclude(orderItem => orderItem.Product)
            .AsNoTracking()
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    { 
        return await dbContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Items)
            .ThenInclude(orderItem => orderItem.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public Task<IReadOnlyList<Order?>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Order order)
    {
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync();
    }
}