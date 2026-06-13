using Fulfillment.Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Fulfillment.Application.Dispatching;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));
        
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        return await handler.HandleAsync((dynamic)query);
    }
}