using Microsoft.EntityFrameworkCore;
using Sales.Domain.Customers;
using Sales.Domain.Orders;
using Sales.Domain.Products;

namespace Sales.Infrastructure.Persistence;

public class SalesDbContext: DbContext
{
    public  SalesDbContext(DbContextOptions options) : base(options)
    {}
    
    public DbSet<Customer> Customers =>  Set<Customer>();
    public DbSet<Product> Products =>  Set<Product>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Sales");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}