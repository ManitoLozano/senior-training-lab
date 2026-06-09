using FluentValidation;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Mappers.Customers;
using Sales.Application.Models.Customers;

namespace Sales.Application.Services.Customers;

public class CustomerService(
    ICustomerRepository customerRepository,
    IValidator<CreateCustomerInput> createCustomerInputValidator
) : ICustomerService
{
    public async Task<IReadOnlyList<CustomerOutput>> GetAllAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        
        return customers
            .Select(customer => customer.ToOutput())
            .ToList();
    }

    public Task<CustomerOutput?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerOutput> AddAsync(CreateCustomerInput input)
    {
        var validationResult = await createCustomerInputValidator.ValidateAsync(input);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var customer = input.ToEntity();
        await customerRepository.AddAsync(customer);

        return customer.ToOutput();
    }

    public Task<CustomerOutput> UpdateAsync(Guid id, UpdateCustomerInput input)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}