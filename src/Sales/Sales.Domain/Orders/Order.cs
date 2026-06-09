using Sales.Domain.Customers;
using Sales.Domain.Enums.OrderStatus;

namespace Sales.Domain.Orders;

public sealed class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }

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
}