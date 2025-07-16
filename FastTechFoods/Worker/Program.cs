using Domain.Interfaces;
using Infrastructure.RabbitMq;
using Infrastructure.RabbitMQ;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.AdicionarPedido;
using Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<PedidoEnviadoWorkService>();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["ConnectionString"]);
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IAdicionarPedidoUseCase, AdicionarPedidoUseCase>();

#region RabbitMQ

builder.Services.Configure<RabbitMqSettings>("PedidoConsumer", builder.Configuration.GetSection("RabbitMQ:Pedido"));

builder.Services.AddSingleton<IMessageConsumer<AdicionarPedidoDto>>(sp =>
{
    var settings = sp.GetRequiredService<IOptionsMonitor<RabbitMqSettings>>().Get("PedidoConsumer");
    var factory = new ConnectionFactory
    {
        HostName = settings.HostName,
        UserName = settings.UserName,
        Password = settings.Password,
        VirtualHost = settings.VirtualHost
    };

    return new RabbitMQMessageConsumer<AdicionarPedidoDto>(() => factory.CreateConnectionAsync(), settings);
});

#endregion

var host = builder.Build();
host.Run();
