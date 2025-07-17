using Domain.Interfaces;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.AdicionarPedido;

namespace Worker;

public class PedidoEnviadoWorkService : BackgroundService
{
    private readonly IMessageConsumer<AdicionarPedidoDto> _messageConsumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public PedidoEnviadoWorkService(IMessageConsumer<AdicionarPedidoDto> messageConsumer, IServiceScopeFactory scopeFactory)
    {
        _messageConsumer = messageConsumer;
        _scopeFactory = scopeFactory;

        _messageConsumer.OnMessageReceived += ProcessarMensagem;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageConsumer.ConsumeAsync();
    }

    private async Task ProcessarMensagem(AdicionarPedidoDto adicionarPedidoDto)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var adicionarPedidoUseCase = scope.ServiceProvider.GetRequiredService<IAdicionarPedidoUseCase>();

            adicionarPedidoUseCase.Adicionar(adicionarPedidoDto.ClienteId, adicionarPedidoDto.Id, new AdicionarPedidoDto
            {
                Id = adicionarPedidoDto.Id,
                RestauranteId = adicionarPedidoDto.RestauranteId,
                ClienteId = adicionarPedidoDto.ClienteId,
                Status = adicionarPedidoDto.Status,
                FormaDeEntrega = adicionarPedidoDto.FormaDeEntrega,
                ValorTotal = adicionarPedidoDto.ValorTotal,
                ItensDePedido = adicionarPedidoDto.ItensDePedido
                    .Select(i => new AdicionarItemDePedidoDto
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        ValorUnitario = i.ValorUnitario,
                        Quantidade = i.Quantidade,
                        ValorTotal = i.ValorTotal
                    }).ToList()
            });
        }

        await Task.CompletedTask;
    }
}
