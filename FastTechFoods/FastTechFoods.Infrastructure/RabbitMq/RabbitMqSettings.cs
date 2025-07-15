namespace Infrastructure.RabbitMq;

public class RabbitMqSettings
{
    public required string VirtualHost { get; set; }
    public required string Exchange { get; set; }
    public required string RoutingKey { get; set; }
    public required string HostName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}