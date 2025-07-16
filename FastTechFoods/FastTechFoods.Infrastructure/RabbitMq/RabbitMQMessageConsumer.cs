using Domain.Interfaces;
using Infrastructure.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.RabbitMQ;

public class RabbitMQMessageConsumer<T> : IMessageConsumer<T> where T : class
{
    private readonly Func<Task<IConnection>> _connectionFactory;
    private readonly RabbitMqSettings _settings;

    public event Func<T, Task>? OnMessageReceived;

    public RabbitMQMessageConsumer(Func<Task<IConnection>> connectionFactory, RabbitMqSettings settings)
    {
        _connectionFactory = connectionFactory;
        _settings = settings;
    }

    public async Task ConsumeAsync()
    {
        try
        {
            var connection = await _connectionFactory();

            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var deserialized = JsonSerializer.Deserialize<T>(message);

                    if (OnMessageReceived != null && deserialized != null)
                    {
                        await OnMessageReceived(deserialized);
                        await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.StackTrace);
                    await channel.BasicNackAsync(eventArgs.DeliveryTag, false, false);
                }
            };

            await channel.BasicConsumeAsync(
                    queue: _settings.Queue,
                    autoAck: false,
                    consumer: consumer);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

