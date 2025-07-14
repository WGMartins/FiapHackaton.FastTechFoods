using Domain.PedidoAggregate;
using UseCase.PedidoUseCase.Shared;

namespace UseCase.PedidoUseCase.CriarPedido
{
    public class AdicionarPedidoDto
    {
        public required Guid RestauranteId { get; set; }
        public FormaDeEntrega FormaDeEntrega { get; set; }
        public required IList<AdicionarItemPedidoDto> ItensDePedido { get; set; }
    }
}
