using Sales.Domain.Enums.OrderStatus;

namespace Sales.Application.Models.Orders;

public sealed record OrderOutput(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    OrderStatus Status,
    decimal TotalAmount
);