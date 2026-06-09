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
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderDto order)
    {
        var input = new CreateOrderInput(
            order.CustomerId,
            order.TotalAmount
        );
        
        var newOrder = await orderService.AddOrderAsync(input);
        return Created(newOrder?.Id.ToString(), newOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderDto order)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return NoContent();
    }
}