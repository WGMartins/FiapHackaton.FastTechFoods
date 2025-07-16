namespace UseCase.PedidoUseCase.AdicionarPedido;

public class AdicionarItemDePedidoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal ValorUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
}
