using Fulfillment.Application.Abstractions.Messaging;

namespace Fulfillment.Application.Commands.OrderProcessing;

public sealed record CompleteOrderProcessingCommand(Guid OrderProcessingId): ICommand;