using Fulfillment.Application.Abstractions.Messaging;
using Fulfillment.Application.Commands.OrderProcessing;
using Fulfillment.Application.Interfaces.Repositories;

namespace Fulfillment.Application.Handlers.Commands.OrderProcessings;

public class FailOrderProcessingCommandHandler(IOrderProcessingRepository orderProcessingRepository): ICommandHandler<FailOrderProcessingCommand>
{
    public async Task HandleAsync(FailOrderProcessingCommand command)
    {
        var orderProcessing = await orderProcessingRepository.GetByIdAsync(command.OrderProcessingId);
        if (orderProcessing is null) return;
        
        orderProcessing.Fail(command.Reason);
        await orderProcessingRepository.UpdateAsync(orderProcessing);
    }
}