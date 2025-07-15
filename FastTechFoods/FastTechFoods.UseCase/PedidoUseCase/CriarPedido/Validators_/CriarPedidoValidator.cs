using FluentValidation;
using UseCase.PedidoUseCase.Shared;

namespace UseCase.PedidoUseCase.CriarPedido;

public class CriarPedidoValidator : AbstractValidator<AdicionarPedidoDto>
{
    public CriarPedidoValidator()
    {
        
    }
}
