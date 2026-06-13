using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Commands.OrderProcessing;
using Fulfillment.Application.Interfaces.Repositories;
using Fulfillment.Domain.OrderProcessing;

namespace Fulfillment.Application.Handlers.Commands.OrderProcessings;

public sealed class CreateOrderProcessingCommandHandler(IOrderProcessingRepository orderProcessingRepository): ICommandHandler<CreateOrderProcessingCommand>
{
    public async Task HandleAsync(CreateOrderProcessingCommand command)
    {
        var existingOrderProcessing = await orderProcessingRepository.GetByOrderIdAsync(command.OrderId);
        if (existingOrderProcessing is not null) return;

        var orderProcessing = new OrderProcessing(command.OrderId);
        await orderProcessingRepository.AddAsync(orderProcessing);
    }
}