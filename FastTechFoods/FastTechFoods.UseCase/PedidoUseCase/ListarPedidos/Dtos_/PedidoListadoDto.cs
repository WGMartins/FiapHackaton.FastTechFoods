using Domain.PedidoAggregate;

namespace UseCase.PedidoUseCase.ListarPedidos
{
    public class PedidoListadoDto
    {
        public Guid Id { get; set; }
        public required ClienteDto Cliente { get; set; }
        public Status Status { get; set; }
        public FormaDeEntrega FormaDeEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public required IList<ItemDePedidoDto> ItensDePedido { get; set; }
    }
}
