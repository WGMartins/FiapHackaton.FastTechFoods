using UseCase.PedidoUseCase.CriarPedido;

namespace UseCase.Interfaces
{
    public interface ICriarPedidoUseCase
    {
        PedidoAdicionadoDto Criar (AdicionarPedidoDto input);
    }
}
