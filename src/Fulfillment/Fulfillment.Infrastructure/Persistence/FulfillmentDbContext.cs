using Fulfillment.Domain;
using Fulfillment.Domain.OrderProcessing;
using Microsoft.EntityFrameworkCore;

namespace Fulfillment.Infrastructure.Persistence;

public class FulfillmentDbContext: DbContext
{
    public  FulfillmentDbContext(DbContextOptions options) : base(options)
    {}
    
    public DbSet<OrderProcessing> OrderProcessings => Set<OrderProcessing>();
    public DbSet<OrderProcessingHistory> OrderProcessingHistory => Set<OrderProcessingHistory>();
    public DbSet<OrderProcessingItem> OrderProcessingItems => Set<OrderProcessingItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("fulfillment");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FulfillmentDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}