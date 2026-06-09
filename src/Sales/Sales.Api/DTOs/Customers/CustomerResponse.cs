namespace Sales.Api.DTOs.Customers;

public sealed record CustomerResponse(
  Guid Id,
  string Name,
  string Email,
  string Document,
  DateTime CreatedAt
);