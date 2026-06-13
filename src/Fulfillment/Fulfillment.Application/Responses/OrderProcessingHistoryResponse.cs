namespace Fulfillment.Application.Responses;

public sealed record OrderProcessingHistoryResponse(
    Guid Id,
    Guid OrderProcessingId,
    string Status,
    string Description,
    DateTime CreatedAt
);