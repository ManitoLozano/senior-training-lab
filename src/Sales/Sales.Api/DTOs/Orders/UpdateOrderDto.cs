namespace Sales.Api.DTOs.Orders;

public sealed record UpdateOrderDto(
    Guid CustomerId,
    decimal TotalAmount    
);