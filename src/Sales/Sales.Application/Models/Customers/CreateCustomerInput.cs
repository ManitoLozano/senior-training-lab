namespace Sales.Application.Models.Customers;

public sealed record CreateCustomerInput(
    string Name, 
    string Email,
    string Document
);