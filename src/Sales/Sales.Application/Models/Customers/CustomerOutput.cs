namespace Sales.Application.Models.Customers;

public sealed record CustomerOutput(
    Guid Id,
    string Name,
    string Email,
    string Document
);