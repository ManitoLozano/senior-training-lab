using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Commands.OrderProcessing;
using Fulfillment.Application.Dispatching;
using Fulfillment.Application.Handlers.Commands.OrderProcessings;
using Fulfillment.Application.Handlers.Queries.OrderProcessings;
using Fulfillment.Application.Queries.OrderProcessing;
using Fulfillment.Application.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace Fulfillment.Application.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddFulfillmentApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddScoped<ICommandHandler<CreateOrderProcessingCommand>, CreateOrderProcessingCommandHandler>();
        services.AddScoped<ICommandHandler<CompleteOrderProcessingCommand>, CompleteOrderProcessingCommandHandler>();
        services.AddScoped<ICommandHandler<FailOrderProcessingCommand>, FailOrderProcessingCommandHandler>();
        services.AddScoped<IQueryHandler<GetOrderProcessingByIdQuery, OrderProcessingResponse?>, GetOrderProcessingByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetOrderProcessingByOrderIdQuery, OrderProcessingResponse?>, GetOrderProcessingByOrderIdQueryHandler>();
        services
            .AddScoped<IQueryHandler<GetOrderProcessingByStatusQuery, IReadOnlyList<OrderProcessingResponse>>,
                GetOrderProcessingByStatusQueryHandler>();
        services
            .AddScoped<IQueryHandler<GetOrderProcessingHistoriesByOrderProcessingIdQuery,
                IReadOnlyList<OrderProcessingHistoryResponse>>, GetOrderProcessingHistoriesByOrderProcessingIdQueryHandler>();

        return services;
    }
}