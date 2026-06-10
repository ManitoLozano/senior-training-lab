namespace Sales.Application.Models.Orders;

public sealed record OrderItemOutput(
    Guid Id,
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);