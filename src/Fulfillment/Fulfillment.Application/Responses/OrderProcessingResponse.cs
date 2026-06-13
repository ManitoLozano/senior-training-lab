using Fulfillment.Domain.OrderProcessing;

namespace Fulfillment.Application.Responses;

public sealed record OrderProcessingResponse(
    Guid Id,
    Guid OrderId,
    string Status,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    IReadOnlyList<OrderProcessingHistoryResponse> History
);