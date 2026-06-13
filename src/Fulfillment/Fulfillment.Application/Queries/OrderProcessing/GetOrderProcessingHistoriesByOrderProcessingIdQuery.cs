using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Queries.OrderProcessing;

public sealed record GetOrderProcessingHistoriesByOrderProcessingIdQuery(Guid OrderProcessingId): IQuery<IReadOnlyList<OrderProcessingHistoryResponse?>>;