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

    public static Order ToEntity(this CreateOrderInput orderOutput)
    {
        return new Order(
            orderOutput.CustomerId,
            orderOutput.TotalAmount
        );
    }
}