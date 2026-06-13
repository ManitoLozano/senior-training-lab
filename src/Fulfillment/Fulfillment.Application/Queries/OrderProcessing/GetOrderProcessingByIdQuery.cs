using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Responses;

namespace Fulfillment.Application.Queries.OrderProcessing;

public sealed record GetOrderProcessingByIdQuery(Guid OrderProcessingId): IQuery<OrderProcessingResponse>;