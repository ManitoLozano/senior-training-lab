namespace Sales.Application.Events.Orders;

public sealed record OrderCreatedIntegrationEvent(
    Guid EventId,
    Guid OrderId,
    Guid CustomerId,
    DateTime OcurredAt,
    IReadOnlyList<OrderCreatedItemIntregationEvent> Items
);