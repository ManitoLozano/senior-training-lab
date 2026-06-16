namespace Fulfillment.Infrastructure.Messaging;

public class RabbitMqOptions
{
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Exchange { get; init; } = string.Empty;
    public string RoutingKey { get; init; } = string.Empty;
    public string Queue { get; init; } = string.Empty;
}