using Domain.PedidoAggregate;

namespace Domain.CardapioAggregate
{
    public class Restaurante : EntityBase
    {
        public required string Nome { get; set; }
        public Guid CardapioId { get; set; }

        public Cardapio Cardapio { get; set; }

        public IList<Pedido> Pedidos { get; set; }
    }
}
