using Microsoft.AspNetCore.Mvc;
using Sales.Api.DTOs.Products;
using Sales.Application.Interfaces.Products;
using Sales.Application.Models.Products;
using Sales.Domain.Products;

namespace Sales.Api.Controllers.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetById(int id)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
    
    [HttpPost]
    public async Task <IActionResult> Create([FromBody] CreateProductDto product)
    {
        var input = new CreateProductInput(
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity
        );
        
        var newProduct = await productService.AddAsync(input); 
        return Created(newProduct.Id.ToString(), newProduct);
    }

    [HttpPut("{id}")]
    public Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}