using Sales.Application.Models.Products;
using Sales.Domain.Products;

namespace Sales.Application.Mappers.Products;

public static class ProductMapper
{
    public static ProductOutput ToOutput(this Product product)
    {
        return new ProductOutput(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity
        );
    }

    public static Product ToEntity(this CreateProductInput createProductInput)
    {
        return new Product(
            createProductInput.Name,
            createProductInput.Description,
            createProductInput.Price,
            createProductInput.StockQuantity
        );
    }
}