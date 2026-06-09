using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Customers;

namespace Sales.Infrastructure.Persistence.Configurations.Customers;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "Sales");
        
        builder.HasKey(customer => customer.Id);
        
        builder.Property(customer => customer.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();
        
        builder.Property(customer => customer.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(customer => customer.Email)
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(customer => customer.Document)
            .HasColumnName("Document")
            .IsRequired()
            .HasMaxLength(14);
        
        builder.HasIndex(customer => customer.Document).IsUnique();
        builder.HasIndex(customer => customer.Email).IsUnique();
        
        builder.Property(customer => customer.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}