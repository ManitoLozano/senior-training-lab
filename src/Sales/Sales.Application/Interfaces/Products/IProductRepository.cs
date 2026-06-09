using Sales.Domain.Products;

namespace Sales.Application.Interfaces.Products;

public interface IProductRepository
{
    Task<IReadOnlyList<Product?>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}