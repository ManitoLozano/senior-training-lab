namespace Sales.Application.Models.Products;

public sealed record UpdateProductInput(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);