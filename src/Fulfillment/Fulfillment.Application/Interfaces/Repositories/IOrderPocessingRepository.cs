using Fulfillment.Domain.OrderProcessing;

namespace Fulfillment.Application.Interfaces.Repositories;

public interface IOrderProcessingRepository
{
    Task<OrderProcessing?> GetByOrderIdAsync(Guid orderId);
    Task AddAsync(OrderProcessing orderProcessing);
}