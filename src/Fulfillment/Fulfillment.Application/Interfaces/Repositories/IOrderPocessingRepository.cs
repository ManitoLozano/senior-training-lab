using Fulfillment.Domain.OrderProcessing;

namespace Fulfillment.Application.Interfaces.Repositories;

public interface IOrderProcessingRepository
{
    Task<OrderProcessing?> GetByIdAsync(Guid id);
    Task<OrderProcessing?> GetByOrderIdAsync(Guid orderId);
    Task<IReadOnlyList<OrderProcessing>> GetByStatusAsync(string status);
    Task<IReadOnlyList<OrderProcessingHistory>> GetOrderProcessingHistoriesByOrderProcessingIdAsync(Guid orderProcessingId);
    Task AddAsync(OrderProcessing orderProcessing);
    Task UpdateAsync(OrderProcessing orderProcessing);
}