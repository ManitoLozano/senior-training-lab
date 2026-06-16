namespace Fulfillment.Domain.OrderProcessing;

public sealed class OrderProcessing
{
    private readonly List<OrderProcessingHistory> _histories = [];
    private readonly List<OrderProcessingItem> _items = [];
    
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Status { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    public IReadOnlyList<OrderProcessingHistory> Histories => _histories;
    public IReadOnlyList<OrderProcessingItem> Items => _items;
    
    private OrderProcessing(){}

    public OrderProcessing(Guid orderId, IReadOnlyList<OrderProcessingItemData> items)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = "Started";
        CreatedAt = DateTime.UtcNow;

        foreach (var item in items)
        {
            AddItem(item.ProductId, item.Quantity, item.UnitPrice);
        }
        
        AddHistory(Status, "Order processing started");
    }

    public void Complete()
    {
        Status = "Completed";
        CompletedAt = DateTime.UtcNow;
        
        AddHistory(Status, "Order processing completed");
    }

    public void Fail(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentNullException(nameof(reason), "The reason cannot be null or whitespace.");
        Status = "Failed";
        AddHistory(Status, reason);
    }

    private void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        _items.Add(new OrderProcessingItem(Id, productId, quantity, unitPrice));
    }

    private void AddHistory(string status, string description)
    {
        _histories.Add(new OrderProcessingHistory(Id, status, description));
    }
}