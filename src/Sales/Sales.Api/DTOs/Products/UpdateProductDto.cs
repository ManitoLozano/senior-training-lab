namespace Sales.Api.DTOs.Products;

public sealed record UpdateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);