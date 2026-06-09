using Sales.Domain.Products;

namespace Sales.Domain.Orders;

public sealed class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = null!;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    
    private OrderItem() {}

    // aqui permitimos que apenas order crie seu OrderItem mas sem ele não criamos
    internal OrderItem(Order order, Product product, decimal unitPrice, int quantity)
    {
        EnsurerOrder(order);
        EnsurerProduct(product);
        EnsurerUnitPrice(unitPrice);
        EnsurerQuantity(quantity);
        EnsurerTotalPrice(unitPrice * quantity);
        
        Id = Guid.NewGuid();
        Order = order;
        OrderId = order.Id;
        Product = product;
        ProductId = product.Id;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = unitPrice * quantity;
    }

    private static void EnsurerOrder(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order), "Order can´t be null");
    }
    
    private static void EnsurerProduct(Product product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product), "Product is null");
    }

    private static void EnsurerUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero");
    }

    private static void EnsurerQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");
    }

    private static void EnsurerTotalPrice(decimal totalPrice)
    {
        if (totalPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalPrice), "TotalPrice must be greater than zero");
    }
}