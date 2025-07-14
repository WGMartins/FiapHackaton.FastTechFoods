namespace Domain.PedidoAggregate
{
    public class Cliente : EntityBase
    {
        public required string Nome { get; set; }

        public IList<Pedido> Pedidos { get; set; }
    }
}
