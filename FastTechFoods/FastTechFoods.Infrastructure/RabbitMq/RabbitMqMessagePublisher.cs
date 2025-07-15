using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using UseCase.Interfaces;

namespace Infrastructure.RabbitMq;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly Func<Task<IConnection>> _connectionFactory;
    private readonly IOptions<RabbitMqSettings> _settings;

    public RabbitMqMessagePublisher(Func<Task<IConnection>> connectionFactory, IOptions<RabbitMqSettings> settings)
    {
        _connectionFactory = connectionFactory;
        _settings = settings;
    }

    public async Task PublishAsync<T>(T message)
    {
        try
        {
            var connection = await _connectionFactory();

            using var channel = await connection.CreateChannelAsync();
            {
                string jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var props = new BasicProperties()
                {
                    ContentType = "application/json",
                    DeliveryMode = DeliveryModes.Persistent,
                };

                await channel.BasicPublishAsync(
                    exchange: _settings.Value.Exchange,
                    routingKey: _settings.Value.RoutingKey,
                    mandatory: false,
                    basicProperties: props,
                    body: (body));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}