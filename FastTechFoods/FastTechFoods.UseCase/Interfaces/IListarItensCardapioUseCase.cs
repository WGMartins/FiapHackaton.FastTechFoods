using Domain.CardapioAggregate;
using UseCase.CardapioUseCase.ListarItensCardapio;

namespace UseCase.Interfaces
{
    public interface IListarItensCardapioUseCase
    {
        IList<ItemListadosDto> Listar(Guid idCliente, Guid idCardapio, string? nome, TipoRefeicao? tipoRefeicao);
    }
}
