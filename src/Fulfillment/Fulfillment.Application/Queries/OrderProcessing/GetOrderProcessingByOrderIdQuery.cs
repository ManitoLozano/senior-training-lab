using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Queries.OrderProcessing;

public sealed record GetOrderProcessingByOrderIdQuery(Guid OrderId): IQuery<OrderProcessingResponse>;
