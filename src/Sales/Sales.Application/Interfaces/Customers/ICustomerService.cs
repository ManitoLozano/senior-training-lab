using Sales.Application.Models.Customers;
using Sales.Domain.Customers;

namespace Sales.Application.Interfaces.Customers;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerOutput>> GetAllAsync();
    Task<CustomerOutput?> GetByIdAsync(Guid id);
    Task<CustomerOutput> AddAsync(CreateCustomerInput input);
    Task<CustomerOutput> UpdateAsync(Guid id, UpdateCustomerInput input);
    Task DeleteAsync(Guid id);
}