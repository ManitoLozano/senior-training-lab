using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Orders;

namespace Sales.Infrastructure.Persistence.Configurations.Orders;

public class OrderConfiguration: IEntityTypeConfiguration<Order> {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "Sales");
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();
        
        builder.Property(o => o.CustomerId)
            .HasColumnName("CustomerId")
            .IsRequired();

        builder.Property(o => o.Status)
            .HasColumnName("Status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(o => o.TotalAmount)
            .HasColumnName("TotalAmount")
            .HasPrecision(18,2)
            .IsRequired();
        
        builder.HasOne(o => o.Customer)
            .WithMany() // Não existe propriedade para mostrar o OrdersWithCustomers pq a relação n gera uma entidade
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // Assim o banco consegue remover os orderItems com o orderId igual
        
        // o EF quando trazer os itens consegue preencher nosso _itens private do Order.cs
        builder.Navigation(o => o.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}