using Domain.CardapioAggregate;

namespace Domain.PedidoAggregate
{
    public class Pedido : EntityBase
    {
        public Guid RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Status Status { get; set; }
        public FormaDeEntrega FormaDeEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public IList<ItemDePedido> ItensDePedido { get; set; } = [];

        protected Pedido(Guid restauranteId, Guid clienteId, FormaDeEntrega formaDeEntrega)
        {
            RestauranteId = restauranteId;
            ClienteId = clienteId;
            Status = Status.EmAndamento;
            FormaDeEntrega = formaDeEntrega;
        }

        public static Pedido Criar(Guid restauranteId, Guid clienteId, FormaDeEntrega formaDeEntrega)
        {
            return new Pedido(restauranteId, clienteId, formaDeEntrega);
        }

        private void AtualizarValorTotal (decimal valorTotalItem)
        {
            ValorTotal += valorTotalItem;
        }

        public ItemDePedido AdicionarItem(Guid pedidoId, string nome, decimal valorUnitario, int quantidade)
        {
            var itemDePedido = ItemDePedido.Criar(pedidoId, nome, valorUnitario, quantidade);

            ItensDePedido.Add(itemDePedido);
            
            AtualizarValorTotal(itemDePedido.ValorTotal);

            return itemDePedido;
        }

        public void AlterarStatus(Status status)
        {
            Status = status;
        }
    }
}
