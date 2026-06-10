namespace Sales.Api.DTOs.Orders;

public sealed record CreateOrderDto(
    Guid CustomerId,
    IReadOnlyList<CreateOrderItemDto> OrderItems
);