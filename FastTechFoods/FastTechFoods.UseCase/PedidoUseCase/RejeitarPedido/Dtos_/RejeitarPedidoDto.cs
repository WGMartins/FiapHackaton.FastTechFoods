using Domain.PedidoAggregate;

namespace UseCase.PedidoUseCase.RejeitarPedido;

public class RejeitarPedidoDto
{
    public Guid Id { get; set; }
    public Status Status {  get; set; }
}
