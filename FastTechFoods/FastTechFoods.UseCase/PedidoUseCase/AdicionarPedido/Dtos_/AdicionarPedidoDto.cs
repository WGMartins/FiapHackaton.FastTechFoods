using Domain.PedidoAggregate;

namespace UseCase.PedidoUseCase.AdicionarPedido;

public class AdicionarPedidoDto
{
    public Guid Id { get; set; }
    public Guid RestauranteId { get; set; }
    public Guid ClienteId { get; set; }
    public Status Status { get; set; }
    public FormaDeEntrega FormaDeEntrega { get; set; }
    public decimal ValorTotal { get; set; }
    public IList<AdicionarItemDePedidoDto> ItensDePedido { get; set; } = [];
}
