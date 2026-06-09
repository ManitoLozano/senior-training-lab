using Sales.Domain.Enums.OrderStatus;

namespace Sales.Api.DTOs.Orders;

public sealed record CreateOrderDto(
    Guid CustomerId,
    decimal TotalAmount
);