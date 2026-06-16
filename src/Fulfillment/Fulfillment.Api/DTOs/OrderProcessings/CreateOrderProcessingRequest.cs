namespace Fulfillment.Api.DTOs.OrderProcessings;

public sealed record CreateOrderProcessingRequest(
    Guid OrderId,
    IReadOnlyList<CreateOrderProcessingItemRequest> Items
);