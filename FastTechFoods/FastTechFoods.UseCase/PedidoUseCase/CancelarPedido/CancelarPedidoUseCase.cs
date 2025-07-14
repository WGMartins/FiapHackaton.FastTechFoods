using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.CancelarPedido
{
    public class CancelarPedidoUseCase : ICancelarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public CancelarPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public void Cancelar(Guid idCliente, Guid id)
        {
            var pedido = _pedidoRepository.ObterPorId(id);

            if (pedido is null || pedido.ClienteId != idCliente)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (pedido.Status != Status.EmAndamento && pedido.Status != Status.Enviado)
            {
                throw new Exception("Pedido não permite cancelamento");
            }

            pedido.AlterarStatus(Status.Cancelado);

            _pedidoRepository.Atualizar(pedido);
        }
    }
}
