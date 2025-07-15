using UseCase.PedidoUseCase.CriarPedido;

namespace UseCase.Interfaces
{
    public interface ICriarPedidoUseCase
    {
        PedidoAdicionadoDto Criar (Guid idCliente, AdicionarPedidoDto adicionarPedidoDto);
    }
}
