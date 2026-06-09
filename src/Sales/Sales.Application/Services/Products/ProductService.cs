using FluentValidation;
using Sales.Application.Interfaces.Products;
using Sales.Application.Mappers.Products;
using Sales.Application.Models.Products;

namespace Sales.Application.Services.Products;

public class ProductService(
    IProductRepository productRepository,
    IValidator<CreateProductInput> createProductValidator
) : IProductService
{
    public async Task<IReadOnlyList<ProductOutput>> GetAllAsync()
    {
        var products = await  productRepository.GetAllAsync();

        return products
            .Select(p => p.ToOutput())
            .ToList();
    }

    public Task<ProductOutput> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductOutput> AddAsync(CreateProductInput input)
    {
        var validationResult = await  createProductValidator.ValidateAsync(input);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = input.ToEntity();
        await productRepository.AddAsync(product);
        
        return product.ToOutput();
    }

    public Task<ProductOutput> UpdateAsync(Guid id, UpdateProductInput input)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}