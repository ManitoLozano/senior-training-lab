namespace Sales.Application.Models.Products;

public sealed record ProductOutput(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);