using System.Collections.Immutable;
using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Application.Queries.OrderProcessing;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Handlers.Queries.OrderProcessings;

public class GetOrderProcessingByStatusQueryHandler(IOrderProcessingRepository repository): IQueryHandler<GetOrderProcessingByStatusQuery, IReadOnlyList<OrderProcessingResponse>>
{
    public async Task<IReadOnlyList<OrderProcessingResponse>> HandleAsync(GetOrderProcessingByStatusQuery query)
    {
        var orderProcessings = await repository.GetByStatusAsync(query.Status);

        return orderProcessings.Select(orderProcessing => new OrderProcessingResponse(
            orderProcessing.Id,
            orderProcessing.OrderId,
            orderProcessing.Status,
            orderProcessing.CreatedAt,
            orderProcessing.CompletedAt,
            orderProcessing.Histories.Select(history => new OrderProcessingHistoryResponse(
              history.Id,
              history.OrderProcessingId,
              history.Status,
              history.Description,
              history.CreatedAt
            )).ToList()
        )).ToList();
    }
}