namespace Sales.Application.Models.Orders;

public sealed record UpdateOrderItemInput(
    Guid? Id,
    Guid ProductId,
    int Quantity
);