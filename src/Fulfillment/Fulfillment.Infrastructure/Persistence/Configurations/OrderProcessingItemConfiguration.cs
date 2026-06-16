using Fulfillment.Domain.OrderProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fulfillment.Infrastructure.Persistence.Configurations;

public class OrderProcessingItemConfiguration: IEntityTypeConfiguration<OrderProcessingItem>
{
    public void Configure(EntityTypeBuilder<OrderProcessingItem> builder)
    {
        builder.ToTable("OrderProcessingItems");
        builder.HasKey(orderProcessingItem => orderProcessingItem.Id);

        builder.Property(orderProcessingItem => orderProcessingItem.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(orderProcessingItem => orderProcessingItem.ProductId)
            .HasColumnName("ProductId")
            .IsRequired();
        
        builder.Property(orderProcessingItem => orderProcessingItem.Quantity)
            .HasColumnName("Quantity")
            .IsRequired();
        
        builder.Property(orderProcessingItem => orderProcessingItem.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasPrecision(18, 2)
            .IsRequired();
        
        builder.Property(orderProcessingItem => orderProcessingItem.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasPrecision(18, 2)
            .IsRequired();
    }
}