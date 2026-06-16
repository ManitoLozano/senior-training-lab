namespace Fulfillment.Domain.OrderProcessing;

public class OrderProcessingItem
{
    public Guid Id { get; private set; }
    public Guid OrderProcessingId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    
    private OrderProcessingItem(){}

    public OrderProcessingItem(Guid orderProcessingId, Guid productId, int quantity, decimal unitPrice)
    {
        EnsuranceOrderProcessingId(orderProcessingId);
        EnsuranceProductId(productId);
        EnsuranceQuantity(quantity);
        EnsuranceUnitPrice(unitPrice);
        
        Id = Guid.NewGuid();
        OrderProcessingId = orderProcessingId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = Quantity * UnitPrice;
    }

    private static void EnsuranceOrderProcessingId(Guid orderProcessingId)
    {
        if (orderProcessingId == Guid.Empty)
            throw new ArgumentException($"{nameof(orderProcessingId)} cannot be empty", nameof(orderProcessingId));
    }

    private static void EnsuranceProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException($"{nameof(productId)} cannot be empty", nameof(productId));
    }

    private static void EnsuranceQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException($"{nameof(quantity)} cannot be less or equal to zero", nameof(quantity));
    }

    private static void EnsuranceUnitPrice(decimal unitPrice)
    {
        if (unitPrice <= 0)
            throw new ArgumentException($"{nameof(unitPrice)} cannot be less or equal to zero", nameof(unitPrice));
    }
}