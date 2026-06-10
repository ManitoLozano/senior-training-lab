namespace Sales.Api.DTOs.Orders;

public sealed record UpdateOrderDto(
    Guid CustomerId,
    IReadOnlyList<UpdateOrderItemDto> OrderItems
);