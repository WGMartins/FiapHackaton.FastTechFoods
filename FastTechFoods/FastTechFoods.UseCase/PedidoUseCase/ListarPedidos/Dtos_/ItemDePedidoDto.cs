namespace UseCase.PedidoUseCase.ListarPedidos
{
    public class ItemDePedidoDto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
