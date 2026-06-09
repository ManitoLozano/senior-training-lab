namespace Sales.Api.DTOs.Products;

public sealed record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);