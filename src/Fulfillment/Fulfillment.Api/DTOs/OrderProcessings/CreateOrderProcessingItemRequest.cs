namespace Fulfillment.Api.DTOs.OrderProcessings;

public sealed record CreateOrderProcessingItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);