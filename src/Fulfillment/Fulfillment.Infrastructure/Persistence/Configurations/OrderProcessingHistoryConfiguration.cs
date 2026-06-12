using Fulfillment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fulfillment.Infrastructure.Persistence.Configurations;

public class OrderProcessingHistoryConfiguration: IEntityTypeConfiguration<OrderProcessingHistory>
{
    public void Configure(EntityTypeBuilder<OrderProcessingHistory> builder)
    {
        builder.ToTable("order_processing_histories");
        builder.HasKey(processingHistory => processingHistory.Id);
        
        builder.Property(processingHistory => processingHistory.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(processingHistory => processingHistory.OrderProcessingId)
            .HasColumnName("OrderProcessingId")
            .IsRequired();
        
        builder.Property(processingHistory => processingHistory.Status)
            .HasColumnName("Status")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(processingHistory => processingHistory.Description)
            .HasColumnName("Description")
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(processingHistory => processingHistory.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
        
    }
}