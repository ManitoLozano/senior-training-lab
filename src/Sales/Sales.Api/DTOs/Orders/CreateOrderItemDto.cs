namespace Sales.Api.DTOs.Orders;

public sealed record CreateOrderItemDto(
    Guid ProductId,
    int Quantity
);