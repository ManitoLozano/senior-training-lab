using Sales.Domain.Enums.OrderStatus;

namespace Sales.Application.Models.Orders;

public sealed record UpdateOrderInput(Guid customerId, OrderStatus status, decimal totalAmount);