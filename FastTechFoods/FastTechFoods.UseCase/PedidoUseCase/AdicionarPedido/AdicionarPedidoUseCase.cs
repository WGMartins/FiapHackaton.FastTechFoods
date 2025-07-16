using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.AdicionarPedido;

public class AdicionarPedidoUseCase : IAdicionarPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;    

    public AdicionarPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;               
    }

    public void Adicionar(Guid idCliente, Guid id, AdicionarPedidoDto adicionarPedidoDto)
    {
        Pedido pedido = Pedido.Criar(adicionarPedidoDto.Id,
                                     adicionarPedidoDto.RestauranteId, 
                                     idCliente, 
                                     adicionarPedidoDto.Status, 
                                     adicionarPedidoDto.FormaDeEntrega, 
                                     adicionarPedidoDto.ValorTotal);

        foreach (var itempedido in adicionarPedidoDto.ItensDePedido)
        {
            pedido.AdicionarItem(itempedido.Id, pedido.Id, itempedido.Nome, itempedido.ValorUnitario, itempedido.Quantidade);
        }

        _pedidoRepository.Adicionar(pedido);
    }
}
