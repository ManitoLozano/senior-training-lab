using Sales.Application.Models.Orders;
using Sales.Domain.Orders;

namespace Sales.Application.Mappers.Orders;

public static class OrderMapper
{
    public static OrderOutput ToOutput(this Order order)
    {
        return new OrderOutput(
            order.Id,
            order.Customer.Id,
            order.Customer.Name,
            order.Status,
            order.TotalAmount
        );
    }

    public static OrderItemOutput ToOutput(this OrderItem orderItem)
    {
        return new OrderItemOutput(
            orderItem.Id,
            orderItem.ProductId,
            ProductName: orderItem.Product.Name,
            orderItem.UnitPrice,
            orderItem.Quantity,
            orderItem.TotalPrice
        );
    }
}