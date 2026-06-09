namespace Sales.Application.Models.Products;

public sealed record CreateProductInput(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);