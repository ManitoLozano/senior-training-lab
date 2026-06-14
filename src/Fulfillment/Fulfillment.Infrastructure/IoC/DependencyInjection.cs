using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Infrastructure.Persistence;
using Fulfillment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fulfillment.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddFulfillmentInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FulfillmentDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SeniorTrainingDb"));
        });
        
        services.AddScoped<IOrderProcessingRepository, OrderProcessingRepository>();

        return services;
    }
}