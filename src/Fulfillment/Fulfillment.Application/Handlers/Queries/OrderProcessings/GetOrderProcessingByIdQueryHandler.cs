using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Application.Queries.OrderProcessing;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Handlers.Queries.OrderProcessings;

public class GetOrderProcessingByIdQueryHandler(IOrderProcessingRepository orderProcessingRepository): IQueryHandler<GetOrderProcessingByIdQuery, OrderProcessingResponse?>
{
    public async Task<OrderProcessingResponse?> HandleAsync(GetOrderProcessingByIdQuery query)
    {
        var orderProcessing = await orderProcessingRepository.GetByIdAsync(query.Id);
        if(orderProcessing is null) return null;
        
        return new OrderProcessingResponse(
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
            )).ToList(),
            orderProcessing.Items.Select(item => new OrderProcessingItemResponse(
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                item.TotalPrice
            )).ToList()
        );
    }
}