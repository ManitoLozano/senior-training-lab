using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Commands.OrderProcessing;
using Fulfillment.Application.Interfaces.Repositories;

namespace Fulfillment.Application.Handlers.Commands.OrderProcessings;

public class CompleteOrderProcessingCommandHandler(IOrderProcessingRepository orderProcessingRepository): ICommandHandler<CompleteOrderProcessingCommand>
{
    public async Task HandleAsync(CompleteOrderProcessingCommand command)
    {
        var orderProcessing = await orderProcessingRepository.GetByOrderIdAsync(command.OrderProcessingId);
        if (orderProcessing is null) return;
        
        orderProcessing.Complete();
        await orderProcessingRepository.UpdateAsync(orderProcessing);
    }
}