using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Interfaces.Customers;
using Sales.Application.Interfaces.Orders;
using Sales.Application.Interfaces.Products;
using Sales.Application.Models.Customers;
using Sales.Application.Models.Orders;
using Sales.Application.Models.Products;
using Sales.Application.Services.Customers;
using Sales.Application.Services.Orders;
using Sales.Application.Services.Products;
using Sales.Application.Validators.Customers;
using Sales.Application.Validators.Orders;
using Sales.Application.Validators.Products;

namespace Sales.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSalesApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        
        services.AddScoped<IValidator<CreateCustomerInput>, CreateCustomerInputValidator>();
        services.AddScoped <IValidator<CreateProductInput>, CreateProductValidator> ();
        services.AddScoped<IValidator<CreateOrderInput>, CreateOrderValidator>();
        
        return services;
    }
}