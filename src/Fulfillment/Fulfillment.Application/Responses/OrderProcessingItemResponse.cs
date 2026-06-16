namespace Fulfillment.Application.Responses;

public sealed record OrderProcessingItemResponse(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);