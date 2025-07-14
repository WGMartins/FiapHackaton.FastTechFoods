using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.EnviarPedido
{
    public class EnviarPedidoUseCase : IEnviarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public EnviarPedidoUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public void Enviar(Guid idCliente, Guid id)
        {
            var pedido = _pedidoRepository.ObterPorId(id);

            if (pedido is null || pedido.ClienteId != idCliente)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (pedido.Status != Status.EmAndamento)
            {
                throw new Exception("Pedido não permite envio");
            }

            pedido.AlterarStatus(Status.Enviado);

            _pedidoRepository.Atualizar(pedido);
        }
    }
}
