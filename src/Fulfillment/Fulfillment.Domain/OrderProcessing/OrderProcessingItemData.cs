namespace Fulfillment.Domain.OrderProcessing;

public sealed record OrderProcessingItemData(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);