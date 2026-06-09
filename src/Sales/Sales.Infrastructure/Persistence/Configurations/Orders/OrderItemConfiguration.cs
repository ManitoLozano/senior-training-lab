using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Orders;

namespace Sales.Infrastructure.Persistence.Configurations.Orders;

public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems", "Sales");
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(o => o.OrderId)
            .HasColumnName("OrderId")
            .IsRequired();
        
        builder.Property(o => o.ProductId)
            .HasColumnName("ProductId")
            .IsRequired();
        
        builder.Property(o => o.Quantity)
            .HasColumnName("Quantity")
            .IsRequired();

        builder.Property(o => o.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasPrecision(18, 2)
            .IsRequired();
        
        builder.Property(o => o.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasPrecision(18, 2)
            .IsRequired();
        
        builder.HasOne(o => o.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}