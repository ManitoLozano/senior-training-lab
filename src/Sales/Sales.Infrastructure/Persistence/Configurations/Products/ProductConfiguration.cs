using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Products;

namespace Sales.Infrastructure.Persistence.Configurations.Products;

public class ProductConfiguration
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();
        
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(p => p.Price)
            .HasColumnName("Price")
            .IsRequired()
            .HasPrecision(18,2);
        
        builder.Property(p => p.StockQuantity)
            .HasColumnName("StockQuantity")
            .IsRequired();
        
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}