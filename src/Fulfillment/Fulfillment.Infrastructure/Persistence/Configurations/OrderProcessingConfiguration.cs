using Fulfillment.Domain.OrderProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fulfillment.Infrastructure.Persistence.Configurations;

public class OrderProcessingConfiguration: IEntityTypeConfiguration<OrderProcessing>
{
    public void Configure(EntityTypeBuilder<OrderProcessing> builder)
    {
        builder.ToTable("OrderProcessing");
        builder.HasKey(orderProcessing => orderProcessing.Id);
        
        builder.Property(orderProcessing => orderProcessing.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(orderProcessing => orderProcessing.OrderId)
            .HasColumnName("OrderId")
            .IsRequired();
        
        builder.Property(orderProcessing => orderProcessing.Status)
            .HasColumnName("Status")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(orderProcessing => orderProcessing.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
        
        builder.HasIndex(orderProcessing => orderProcessing.OrderId)
            .IsUnique();
        
        builder.HasMany(orderProcessing => orderProcessing.Histories)
            .WithOne()
            .HasForeignKey(orderProcessing => orderProcessing.OrderProcessingId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(orderProcessing => orderProcessing.Items)
            .WithOne()
            .HasForeignKey(orderProcessing => orderProcessing.OrderProcessingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(orderProcessing => orderProcessing.Histories)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Navigation(orderProcessing => orderProcessing.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}