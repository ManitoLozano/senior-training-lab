using Fulfillment.Application.Abstractions.Messaging;

namespace Fulfillment.Application.Commands.OrderProcessing;

public sealed record CreateOrderProcessingCommand(
    Guid OrderId,
    IReadOnlyList<CreateOrderProcessingItemCommand> Items
): ICommand;