using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Products;
using Sales.Domain.Products;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories.Products;

public class ProductRepository(SalesDbContext context): IProductRepository
{
    public async Task<IReadOnlyList<Product?>> GetAllAsync()
    {
        return await context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }
}