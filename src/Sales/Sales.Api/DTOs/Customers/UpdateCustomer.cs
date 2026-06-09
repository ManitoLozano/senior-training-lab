namespace Sales.Api.DTOs.Customers;

public sealed record UpdateCustomer(
    string Name,
    string Email
);