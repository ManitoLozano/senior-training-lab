using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Queries.OrderProcessing;

public sealed record GetOrderProcessingByStatusQuery(string Status): IQuery<IReadOnlyList<OrderProcessingResponse>>;