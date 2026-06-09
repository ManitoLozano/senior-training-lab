namespace Sales.Domain.Products;

public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity  { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Product(string name, string description, decimal price, int stockQuantity)
    {
        EnsureName(name);
        EnsureDescription(description);
        EnsurePrice(price);
        EnsureStockQuantity(stockQuantity); 
        
        Id = Guid.NewGuid();
        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        StockQuantity = stockQuantity;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        EnsureName(name);
        Name = name.Trim();
    }

    public void SetDescription(string description)
    {
        EnsureDescription(description);
        Description = description.Trim();
    }

    public void SetPrice (decimal price)
    {
        EnsurePrice(price);
        Price = price;
    }

    public void SetStockQuantity(int stockQuantity)
    {
        EnsureStockQuantity(stockQuantity);
        StockQuantity = stockQuantity;
    }

    private static void EnsureName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required");
    }

    private static void EnsureDescription(string description)
    {
        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description is required");
    }

    private static void EnsurePrice(decimal price)
    {
        if(price <= 0)
            throw new ArgumentException("Product price is required");
    }

    private static void EnsureStockQuantity(int stockQuantity)
    {
        if(stockQuantity <= 0)
            throw new ArgumentException("Product stock quantity is required");
    }
}