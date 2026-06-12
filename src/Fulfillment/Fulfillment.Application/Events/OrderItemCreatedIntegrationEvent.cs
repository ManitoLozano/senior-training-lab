namespace Fulfillment.Application.Events;

public sealed record OrderItemCreatedIntegrationEvent(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);