using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Domain.OrderProcessing;
using Fulfillment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fulfillment.Infrastructure.Repositories;

public sealed class OrderProcessingRepository(FulfillmentDbContext dbContext): IOrderProcessingRepository
{
    public async Task<OrderProcessing?> GetByIdAsync(Guid orderProcessingId)
    {
        return await dbContext.OrderProcessings
            .Include(orderProcessing => orderProcessing.Histories)
            .FirstOrDefaultAsync(orderProcessing => orderProcessing.Id == orderProcessingId);
    }
    
    public async Task<OrderProcessing?> GetByOrderIdAsync(Guid orderId)
    {
        return await dbContext.OrderProcessings
            .Include(orderProcessing => orderProcessing.Histories)
            .FirstOrDefaultAsync(history => history.OrderId == orderId);
    }

    public async Task<IReadOnlyList<OrderProcessing>> GetByStatusAsync(string status)
    {
        return await dbContext.OrderProcessings
            .Where(orderProcessing => orderProcessing.Status == status)
            .Include(orderProcessing => orderProcessing.Histories)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<OrderProcessingHistory>> GetOrderProcessingHistoriesByOrderProcessingIdAsync(Guid orderProcessingId)
    {
        return await dbContext.OrderProcessingHistory
            .AsNoTracking()
            .Where(history => history.OrderProcessingId == orderProcessingId)
            .ToListAsync();
    }

    public async Task AddAsync(OrderProcessing orderProcessing)
    {
        await dbContext.OrderProcessings.AddAsync(orderProcessing);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderProcessing orderProcessing)
    {
        dbContext.OrderProcessings.Update(orderProcessing);
        await dbContext.SaveChangesAsync();
    }
}