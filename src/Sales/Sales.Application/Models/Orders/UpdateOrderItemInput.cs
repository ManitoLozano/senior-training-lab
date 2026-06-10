namespace Sales.Application.Models.Orders;

public sealed record UpdateOrderItemInput(
    Guid ProductId,
    int Quantity
);