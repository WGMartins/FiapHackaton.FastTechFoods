namespace Domain.PedidoAggregate
{
    public class ItemDePedido : EntityBase
    {
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }


        protected ItemDePedido(Guid pedidoId, string nome, decimal valorUnitario, int quantidade)
        {
            PedidoId = pedidoId;
            Nome = nome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
            ValorTotal = quantidade * valorUnitario;
        }

        public static ItemDePedido Criar(Guid pedidoId, string nome, decimal valorUnitario, int quantidade)
        {
            return new ItemDePedido(pedidoId, nome, valorUnitario, quantidade);
        }
    }
}
