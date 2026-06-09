namespace Sales.Application.Models.Orders;

public sealed record CreateOrderInput(
    Guid CustomerId,
    decimal TotalAmount
);