using UseCase.CardapioUseCase.AdicionarItemCardapio;

namespace UseCase.Interfaces
{
    public interface IAdicionarItemCardapioUseCase
    {
        ItemAdicionadoDto Adicionar (Guid idRestaurante, Guid idCardapio, AdicionarItemCardapioDto input);
    }
}
