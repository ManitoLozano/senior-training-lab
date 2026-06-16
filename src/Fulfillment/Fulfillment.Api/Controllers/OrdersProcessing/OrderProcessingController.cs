using Fulfillment.Api.DTOs.OrderProcessings;
using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Commands.OrderProcessing;
using Fulfillment.Application.Queries.OrderProcessing;
using Microsoft.AspNetCore.Mvc;

namespace Fulfillment.Api.Controllers.OrdersProcessing;

[ApiController]
[Route("api/order-processings")]
public class OrderProcessingController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetOrderProcessingByIdQuery(id);
        var result = await dispatcher.QueryAsync(query);
        return result is null ? NotFound() : Ok(result); 
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        var query = new GetOrderProcessingByOrderIdQuery(orderId);
        var result = await dispatcher.QueryAsync(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetStatus(string status)
    {
        var query = new GetOrderProcessingByStatusQuery(status);
        var result = await dispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}/histories")]
    public async Task<IActionResult> GetHistories(Guid id)
    {
        var query = new GetOrderProcessingHistoriesByOrderProcessingIdQuery(id);
        var result = await dispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderProcessingRequest request)
    {
        var command = new CreateOrderProcessingCommand(
            request.OrderId,
            request.Items.Select(item => new CreateOrderProcessingItemCommand(
                item.ProductId,
                item.Quantity,
                item.UnitPrice
            )).ToList()
        );
        await dispatcher.SendAsync(command);
        return Accepted();
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var command = new CompleteOrderProcessingCommand(id);
        await dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPatch("{id:guid}/fail")]
    public async Task<IActionResult> Fail(Guid id, FailOrderProcessingRequest request)
    {
        var command = new FailOrderProcessingCommand(id, request.Reason);
        await dispatcher.SendAsync(command);
        return NoContent();
    }
}