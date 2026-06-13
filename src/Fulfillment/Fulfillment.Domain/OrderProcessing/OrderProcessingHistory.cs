namespace Fulfillment.Domain.OrderProcessing;

public class OrderProcessingHistory
{
    public Guid Id { get; private set; }
    public Guid OrderProcessingId { get; private set; }
    public string Status { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public OrderProcessingHistory(Guid orderProcessingId, string status, string description)
    {
        Id = Guid.NewGuid();
        OrderProcessingId = orderProcessingId;
        Status = status;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}