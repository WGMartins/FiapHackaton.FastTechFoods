using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.AceitarPedido
{
    public class AceitarPedidoUseCase : IAceitarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AceitarPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public void Aceitar(Guid idRestaurante, Guid id)
        {
            var pedido = _pedidoRepository.ObterPorId(id);

            if (pedido is null || pedido.RestauranteId != idRestaurante)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (pedido.Status != Status.Enviado)
            {
                throw new Exception("Pedido não permite aceite");
            }

            pedido.AlterarStatus(Status.Aprovado);

            _pedidoRepository.Atualizar(pedido);
        }
    }
}
