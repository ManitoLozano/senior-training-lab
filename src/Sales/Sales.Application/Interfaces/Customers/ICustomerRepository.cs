using Sales.Domain.Customers;

namespace Sales.Application.Interfaces.Customers;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer?>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Customer>> ExistsByEmailOrDocumentAsync(string email, string document);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}