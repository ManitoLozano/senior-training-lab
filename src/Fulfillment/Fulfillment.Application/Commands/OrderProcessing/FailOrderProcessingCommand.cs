using Fulfillment.Application.Abstractions.Messaging;

namespace Fulfillment.Application.Commands.OrderProcessing;

public sealed record FailOrderProcessingCommand(
    Guid OrderProcessingId,
    string Reason
): ICommand;