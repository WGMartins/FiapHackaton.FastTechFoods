using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;
using System.Text.Json.Serialization;
using UseCase.AuthUseCase.AutenticarUsuario;
using UseCase.CardapioUseCase.AdicionarItemCardapio;
using UseCase.CardapioUseCase.AtualizarItemCardapio;
using UseCase.CardapioUseCase.ListarItensCardapio;
using UseCase.CardapioUseCase.RemoverItemCardapio;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.AceitarPedido;
using UseCase.PedidoUseCase.AdicionarItemPedido;
using UseCase.PedidoUseCase.CancelarPedido;
using UseCase.PedidoUseCase.CriarPedido;
using UseCase.PedidoUseCase.EnviarPedido;
using UseCase.PedidoUseCase.ListarPedidos;
using UseCase.PedidoUseCase.RejeitarPedido;
using UseCase.PedidoUseCase.Shared;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

const string serviceName = "FastTechFoods";

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

builder.Services.AddScoped<ICriarPedidoUseCase, CriarPedidoUseCase>();
builder.Services.AddScoped<IValidator<AdicionarPedidoDto>, CriarPedidoValidator>();

builder.Services.AddScoped<IAdicionarItemPedidoUseCase, AdicionarItemPedidoUseCase>();
builder.Services.AddScoped<IValidator<AdicionarItemPedidoDto>, AdicionarItemPedidoValidator>();

builder.Services.AddScoped<IAceitarPedidoUseCase, AceitarPedidoUseCase>();

builder.Services.AddScoped<ICancelarPedidoUseCase, CancelarPedidoUseCase>();

builder.Services.AddScoped<IEnviarPedidoUseCase, EnviarPedidoUseCase>();

builder.Services.AddScoped<IRejeitarPedidoUseCase, RejeitarPedidoUseCase>();

builder.Services.AddScoped<IListarPedidosUseCase, ListarPedidosUseCase>();

#endregion

#region Autenticação
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAutenticarUsuarioUseCase, AutenticarUsuarioUseCase>();
builder.Services.AddScoped<IValidator<AutenticarUsuarioDto>, AutenticarUsuarioValidator>();
builder.Services.AddScoped<JwtToken>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["SecretKey"]))
        };
    });


#endregion

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FastTechFoods", Version = "v1" });

    // Adiciona suporte ao JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT como: Bearer {seu_token_aqui}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

builder.Services.AddAuthorization();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
