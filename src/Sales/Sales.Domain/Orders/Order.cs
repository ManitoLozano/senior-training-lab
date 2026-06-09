using Sales.Domain.Customers;
using Sales.Domain.Enums.OrderStatus;
using Sales.Domain.Products;

namespace Sales.Domain.Orders;

public sealed class Order
{
    // Lista real dos itens do order
    private readonly List<OrderItem> _items = new(); 
        
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    // Cópia dos itens do order onde podemos apenas usar pra leituras, mas não manipular eles 
    public  IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(Guid customerId,  decimal totalAmount)
    {
        EnsuranceCustomerId(customerId);
        EnsuranceTotalAmount(totalAmount);
        
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Created;
        TotalAmount = totalAmount;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product, int quantity)
    {
        EnsuranceProduct(product);
        EnsuranceQuantity(quantity);
        
        var item = new OrderItem(this, product, product.Price, quantity);
        _items.Add(item);

        RecalculateTotalAmountOfItems();
    }

    public void UpdateStatusToCreate()
    {
        Status = OrderStatus.Created;
    }

    public void UpdateStatusToConfirm()
    {
        Status = OrderStatus.Confirmed;    
    }
    
    public void UpdateStatusToCancel()
    {
        Status = OrderStatus.Cancelled;
    }

    public void UpdateStatusToSentToFulfillment()
    {
        Status = OrderStatus.SentToFulfillment;
    }

    public void SetTotalAmount(decimal totalAmount)
    {
        EnsuranceTotalAmount(totalAmount);
        TotalAmount = totalAmount;
    }

    private void RecalculateTotalAmountOfItems()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }

    private static void EnsuranceCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId cannot be empty", nameof(customerId));
    }

    private static void EnsuranceTotalAmount(decimal totalAmount)
    {
        if(totalAmount <= 0)
            throw new ArgumentException("Total amount need to be positive");
    }

    private static void EnsuranceProduct(Product product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product),  "Product cannot be null");
    }

    private static void EnsuranceQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
    }
}