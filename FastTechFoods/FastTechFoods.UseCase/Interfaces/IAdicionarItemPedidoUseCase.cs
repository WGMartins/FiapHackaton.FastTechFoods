using UseCase.PedidoUseCase.AdicionarItemPedido;
using UseCase.PedidoUseCase.Shared;

namespace UseCase.Interfaces
{
    public interface IAdicionarItemPedidoUseCase
    {
        ItemAdicionadoDto Adicionar (Guid idCliente, Guid id, AdicionarItemPedidoDto adicionarItemDto);
    }
}
