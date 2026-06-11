namespace Sales.Application.Events.Orders;

public sealed record OrderCreatedItemIntregationEvent(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);