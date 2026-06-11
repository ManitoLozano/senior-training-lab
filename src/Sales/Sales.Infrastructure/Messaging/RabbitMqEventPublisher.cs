using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Sales.Application.Interfaces.Messaging;

namespace Sales.Infrastructure.Messaging;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly RabbitMqSettings _settings;
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    
    public RabbitMqEventPublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password,
            ClientProvidedName = "sales-api-publisher",
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };
    }

    public async Task PublishAsync<TEvent>(
        TEvent integrationEvent,
        string routingKey,
        CancellationToken cancellationToken = default
    )
    {
        if (integrationEvent is null)
            throw new ArgumentNullException(nameof(integrationEvent));
        
        if (string.IsNullOrWhiteSpace(routingKey))
            throw new ArgumentNullException(nameof(routingKey), "Routing key cannot be empty.");
        
        var connection = await GetConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync(
            exchange: _settings.ExchangeName,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken
        );
        
        var json = JsonSerializer.Serialize(integrationEvent);
        var body = Encoding.UTF8.GetBytes(json);
        var properties = new BasicProperties{ ContentType = "application/json", DeliveryMode = DeliveryModes.Persistent};

        await channel.BasicPublishAsync(
            exchange: _settings.ExchangeName,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken
        );
    }

    private async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true })
            return _connection;

        _connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
        return _connection;
    }

    /*
     * Chamado quando a aplicação parar de forma automática
     * O próprio .net irá chamar esse método para encerrar a conexão com o RabbitMQ
     */
    public async ValueTask DisposeAsync()
    {
        if (_connection is not null)
            await _connection.DisposeAsync();
    }
}