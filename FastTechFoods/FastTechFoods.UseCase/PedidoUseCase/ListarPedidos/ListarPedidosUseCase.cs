using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.ListarPedidos
{
    public class ListarPedidosUseCase : IListarPedidosUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ListarPedidosUseCase(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IList<PedidoListadoDto> Listar(Guid idRestaurante)
        {
            return _pedidoRepository.Listar(idRestaurante).Where(x => x.Status == Status.Enviado).Select(x => Mapper(x)).ToList();
        }

        private PedidoListadoDto Mapper(Pedido pedido)
        {
            return new PedidoListadoDto
            {
                Id = pedido.Id,
                Cliente = new ClienteDto
                {
                    Id = pedido.Cliente.Id,
                    Nome = pedido.Cliente.Nome
                },
                Status = pedido.Status,
                FormaDeEntrega = pedido.FormaDeEntrega,
                ValorTotal = pedido.ValorTotal,
                ItensDePedido = pedido.ItensDePedido
                    .Select(i => new ItemDePedidoDto
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        ValorUnitario = i.ValorUnitario,
                        Quantidade = i.Quantidade,
                        ValorTotal = i.ValorTotal
                    })
                    .ToList()
            };
        }
    }
}
