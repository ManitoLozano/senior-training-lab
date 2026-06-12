namespace Fulfillment.Application.Events;

public sealed record OrderCreatedIntegrationEvent(
    Guid EventId,
    Guid OrderId,
    Guid CustomerId,
    DateTime OccurredAt,
    IReadOnlyList<OrderItemCreatedIntegrationEvent> OrderItems
);