namespace Fulfillment.Domain;

public sealed class OrderProcessing
{
    private readonly List<OrderProcessingHistory> _history;
    
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    public IReadOnlyList<OrderProcessingHistory> Histories => _history;

    public OrderProcessing(Guid orderId)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = "Started";
        CreatedAt = DateTime.UtcNow;
        
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
        Status = "Failed";
        AddHistory(Status, reason);
    }

    public void AddHistory(string status, string description)
    {
        _history.Add(new OrderProcessingHistory(Id, status, description));
    }
}