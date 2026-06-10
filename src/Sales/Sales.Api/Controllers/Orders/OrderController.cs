using Microsoft.AspNetCore.Mvc;
using Sales.Api.DTOs.Orders;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Models.Orders;

namespace Sales.Api.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderDto order)
    {
        var input = new CreateOrderInput(
            order.CustomerId,
            order.OrderItems
                .Select(item => new CreateOrderItemInput(
                    item.ProductId,
                    item.Quantity
                ))
                .ToList()
        );
        
        var newOrder = await orderService.AddOrderAsync(input);
        return Created(newOrder?.Id.ToString(), newOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderDto order)
    {
        var input = new UpdateOrderInput(
            order.CustomerId,
            order.OrderItems.Select(item => new UpdateOrderItemInput(
                item.Id,
                item.ProductId,
                item.Quantity
            ))
            .ToList()
        );
        
        var newOrder = await orderService.UpdateOrderAsync(id, input);
        return Ok(newOrder);
    }

    [HttpPatch("{id}/confirm")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        var order = await orderService.ConfirmAsync(id);
        return Ok(order);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var order = await orderService.CancelAsync(id);
        return Ok(order);
    }

    [HttpPatch("{id}/sentToFulfillment")]
    public async Task<IActionResult> SentToFulfillment(Guid id)
    {
        var order = await orderService.SentToFulfillmentAsync(id);
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}