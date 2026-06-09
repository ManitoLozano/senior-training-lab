namespace Sales.Application.Models.Customers;

public sealed record UpdateCustomerInput(
    string Name,
    string Email
);