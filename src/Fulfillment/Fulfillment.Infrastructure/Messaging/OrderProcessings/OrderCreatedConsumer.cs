using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Fulfillment.Infrastructure.Messaging.OrderProcessings;

public class OrderCreatedConsumer(IOptions<RabbitMqOptions> options, IServiceScope serviceScope, ILogger<OrderCreatedConsumer> logger): BackgroundService
{
    private readonly RabbitMqOptions _rabbitMqOptions = options.Value;
    
    private IConnection? _connection;
    private IChannel? _channel;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
    }

    private async Task CreateConnectionAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.Host,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.Username,
            Password = _rabbitMqOptions.Password,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        
        logger.LogInformation("RabbitMQ connection created");
    }
    
    private async Task ConfigureRabbitMqAsync()
    {
        if (_channel is null)
            throw new InvalidOperationException("RabbitMQ was not created");

        await _channel.ExchangeDeclareAsync(
            exchange: _rabbitMqOptions.Exchange,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false
        );

        await _channel.QueueDeclareAsync(
            queue: _rabbitMqOptions.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        await _channel.QueueBindAsync(
            queue: _rabbitMqOptions.Queue,
            exchange: _rabbitMqOptions.Exchange,
            routingKey: _rabbitMqOptions.RoutingKey
        );

        await _channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false
        );
        
        logger.LogInformation(
            "RabbitMQ configured. Exchange: {ExchangeName}, Queue: {QueueName}, RoutingKey: {RoutingKey}",
            _rabbitMqOptions.Exchange,
            _rabbitMqOptions.Queue,
            _rabbitMqOptions.RoutingKey
        );
    }
}