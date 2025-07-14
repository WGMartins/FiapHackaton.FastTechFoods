using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text.Json.Serialization;
using UseCase.CardapioUseCase.AdicionarItemCardapio;
using UseCase.CardapioUseCase.AtualizarItemCardapio;
using UseCase.CardapioUseCase.ListarItensCardapio;
using UseCase.CardapioUseCase.RemoverItemCardapio;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.AceitarPedido;
using UseCase.PedidoUseCase.AdicionarItemPedido;
using UseCase.PedidoUseCase.CancelarPedido;
using UseCase.PedidoUseCase.EnviarPedido;
using UseCase.PedidoUseCase.ListarPedidos;
using UseCase.PedidoUseCase.RejeitarPedido;
using UseCase.PedidoUseCase.Shared;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

const string serviceName = "TC.Contato.Produto.Inclusao";

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(serviceName))
      .WithTracing(tracing => tracing
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter())
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

#region Banco de Dados

builder.Services.AddScoped<ICardapioRepository, CardapioRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

#endregion

#region Cardapio

builder.Services.AddScoped<IAdicionarItemCardapioUseCase, AdicionarItemCardapioUseCase>();
builder.Services.AddScoped<IValidator<AdicionarItemCardapioDto>, AdicionarItemCardapioValidator>();

builder.Services.AddScoped<IAtualizarItemCardapioUseCase, AtualizarItemCardapioUseCase>();
builder.Services.AddScoped<IValidator<AtualizarItemCardapioDto>, AtualizarItemValidator>();

builder.Services.AddScoped<IRemoverItemCardapioUseCase, RemoverItemCardapioUseCase>();

builder.Services.AddScoped<IListarItensCardapioUseCase, ListarItensCardapioUseCase>();

#endregion

#region Pedido

builder.Services.AddScoped<IAdicionarItemPedidoUseCase, AdicionarItemPedidoUseCase>();
builder.Services.AddScoped<IValidator<AdicionarItemPedidoDto>, AdicionarItemPedidoValidator>();

builder.Services.AddScoped<IAceitarPedidoUseCase, AceitarPedidoUseCase>();

builder.Services.AddScoped<ICancelarPedidoUseCase, CancelarPedidoUseCase>();

builder.Services.AddScoped<IEnviarPedidoUseCase, EnviarPedidoUseCase>();

builder.Services.AddScoped<IRejeitarPedidoUseCase, RejeitarPedidoUseCase>();

builder.Services.AddScoped<IListarPedidosUseCase, ListarPedidosUseCase>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
