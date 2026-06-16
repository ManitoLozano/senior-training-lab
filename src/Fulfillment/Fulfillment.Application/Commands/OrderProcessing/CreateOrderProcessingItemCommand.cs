namespace Fulfillment.Application.Commands.OrderProcessing;

public sealed record CreateOrderProcessingItemCommand(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);