using Sales.Application.Models.Customers;
using Sales.Domain.Customers;

namespace Sales.Application.Mappers.Customers;

public static class CustomerMapper
{
    public static CustomerOutput ToOutput(this Customer customer)
    {
        return new CustomerOutput(
            customer.Id,
            customer.Name,
            customer.Email,
            customer.Document
        );
    }

    public static Customer ToEntity(this CreateCustomerInput input)
    {
        return new Customer(
            input.Name,
            input.Email,
            input.Document
        );
    }
}