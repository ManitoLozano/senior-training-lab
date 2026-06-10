using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Customers;
using Sales.Domain.Customers;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories.Customers;

public class CustomerRepository(SalesDbContext context) : ICustomerRepository
{
    public async Task<IReadOnlyList<Customer?>> GetAllAsync()
    {
        return await context.Customers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await context.Customers
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<IReadOnlyList<Customer>> ExistsByEmailOrDocumentAsync(string email, string document)
    {
        return await context.Customers
            .AsNoTracking()
            .Where(order => order.Email == email || order.Document == document)
            .ToListAsync();
    }

    public async Task AddAsync(Customer customerInput)
    {
        await context.Customers.AddAsync(customerInput);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}