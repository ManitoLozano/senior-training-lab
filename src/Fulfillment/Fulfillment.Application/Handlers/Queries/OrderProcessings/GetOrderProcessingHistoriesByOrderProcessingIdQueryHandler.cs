using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Application.Queries.OrderProcessing;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Handlers.Queries.OrderProcessings;

public class GetOrderProcessingHistoriesByOrderProcessingIdQueryHandler(IOrderProcessingRepository repository): IQueryHandler<GetOrderProcessingHistoriesByOrderProcessingIdQuery, IReadOnlyList<OrderProcessingHistoryResponse?>>
{
    public async Task<IReadOnlyList<OrderProcessingHistoryResponse?>> HandleAsync(GetOrderProcessingHistoriesByOrderProcessingIdQuery query)
    {
        var histories = await repository.GetOrderProcessingHistoriesByOrderProcessingIdAsync(query.OrderProcessingId);

        return histories.Select(history => new OrderProcessingHistoryResponse(
            history.Id,
            history.OrderProcessingId,
            history.Status,
            history.Description,
            history.CreatedAt
        ))
        .ToList();
    }
}