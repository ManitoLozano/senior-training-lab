namespace Sales.Api.DTOs.Orders;

public sealed record UpdateOrderItemDto(
    Guid? Id,
    Guid ProductId,
    int Quantity
);