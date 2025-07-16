using UseCase.PedidoUseCase.AdicionarPedido;

namespace UseCase.Interfaces;

public interface IAdicionarPedidoUseCase
{
    void Adicionar(Guid idCliente, Guid id, AdicionarPedidoDto adicionarItemDto);
}
