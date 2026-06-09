namespace Sales.Api.DTOs.Customers;

public sealed record CreateCustomer(
    string Name,
    string Email,
    string Document
);