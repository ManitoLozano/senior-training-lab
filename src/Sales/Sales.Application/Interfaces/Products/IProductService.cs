using Sales.Application.Models.Products;

namespace Sales.Application.Interfaces.Products;

public interface IProductService
{
    Task<IReadOnlyList<ProductOutput>>  GetAllAsync();
    Task<ProductOutput> GetByIdAsync(int id);
    Task<ProductOutput> AddAsync(CreateProductInput input);
    Task<ProductOutput> UpdateAsync(Guid id, UpdateProductInput input);
    Task DeleteAsync(Guid id);
}