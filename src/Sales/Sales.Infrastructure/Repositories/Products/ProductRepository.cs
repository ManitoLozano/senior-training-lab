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

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}