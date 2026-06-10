namespace Sales.Application.Models.Orders;

public sealed record CreateOrderItemInput(
    Guid ProductId,
    int Quantity
);