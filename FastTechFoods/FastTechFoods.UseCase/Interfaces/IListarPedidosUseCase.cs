using UseCase.PedidoUseCase.ListarPedidos;

namespace UseCase.Interfaces
{
    public interface IListarPedidosUseCase
    {
        IList<PedidoListadoDto> Listar(Guid idRestaurante);
    }
}
