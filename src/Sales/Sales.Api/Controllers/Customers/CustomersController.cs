using Microsoft.AspNetCore.Mvc;
using Sales.Api.DTOs.Customers;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Models.Customers;

namespace Sales.Api.Controllers.Customers;

[ApiController]
[Route("api/[controller]")]
public sealed class CustomersController(ICustomerService service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await service.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await service.GetByIdAsync(id);
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomer customer)
    {
        var input = new CreateCustomerInput(
            customer.Name,
            customer.Email,
            customer.Document
        );

        var newCustomer = await service.AddAsync(input);
        
        return Created(newCustomer.Id.ToString(), newCustomer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCustomer customer)
    {
        // Ação de edição e no retorno mapeamos com o DTO CustomerResponse
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // Ação de deletar
        return NoContent();
    }
}