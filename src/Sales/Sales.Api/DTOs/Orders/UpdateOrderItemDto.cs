namespace Sales.Api.DTOs.Orders;

public sealed record UpdateOrderItemDto(
    Guid ProductId,
    int Quantity
);