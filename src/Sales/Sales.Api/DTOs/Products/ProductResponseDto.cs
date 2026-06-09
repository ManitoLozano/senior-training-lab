namespace Sales.Api.DTOs.Products;

public sealed record ProductResponseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);