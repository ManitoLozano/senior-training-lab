namespace Sales.Application.Models.Orders;

public sealed record UpdateOrderInput(
    Guid CustomerId,
    IReadOnlyList<UpdateOrderItemInput> Items
);