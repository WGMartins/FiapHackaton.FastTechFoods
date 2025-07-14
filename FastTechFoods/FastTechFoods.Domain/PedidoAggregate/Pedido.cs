using Domain.CardapioAggregate;

namespace Domain.PedidoAggregate
{
    public class Pedido : EntityBase
    {
        public Guid RestauranteId { get; set; }
        public required Restaurante Restaurante { get; set; }
        public Guid ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public Status Status { get; set; }
        public FormaDeEntrega FormaDeEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public required IList<ItemDePedido> ItensDePedido { get; set; } = [];

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
