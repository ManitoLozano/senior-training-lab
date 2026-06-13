using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Domain.OrderProcessing;
using Fulfillment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fulfillment.Infrastructure.Repositories;

public sealed class OrderProcessingRepository(FulfillmentDbContext dbContext) : IOrderProcessingRepository
{
    public async Task<OrderProcessing?> GetByOrderIdAsync(Guid orderId)
    {
        return await dbContext.OrderProcessings
            .Include(orderProcessing => orderProcessing.Histories)
            .FirstOrDefaultAsync(history => history.OrderId == orderId);
    }

    public async Task AddAsync(OrderProcessing orderProcessing)
    {
        await dbContext.OrderProcessings.AddAsync(orderProcessing);
        await dbContext.SaveChangesAsync();
    }
}