using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Interfaces.Messaging;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Interfaces.Products;
using Sales.Infrastructure.Messaging;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories.Customers;
using Sales.Infrastructure.Repositories.Orders;
using Sales.Infrastructure.Repositories.Products;

namespace Sales.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSalesInfraestructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<SalesDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SeniorTrainingDb"));
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();

        return services;
    }
}