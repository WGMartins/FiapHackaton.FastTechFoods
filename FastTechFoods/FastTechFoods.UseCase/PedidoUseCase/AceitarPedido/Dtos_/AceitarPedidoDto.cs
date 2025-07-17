using Domain.PedidoAggregate;

namespace UseCase.PedidoUseCase.AceitarPedido;

public class AceitarPedidoDto
{
    public Guid Id { get; set; }    
    public Guid RestauranteId { get; set; }
    public Status Status { get; set; }
}
