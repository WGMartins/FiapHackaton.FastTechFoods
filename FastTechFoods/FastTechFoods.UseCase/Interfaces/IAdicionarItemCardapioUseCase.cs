using UseCase.CardapioUseCase.AdicionarItemCardapio;

namespace UseCase.Interfaces
{
    public interface IAdicionarItemCardapioUseCase
    {
        Task<ItemAdicionadoDto> Adicionar (Guid idRestaurante, Guid idCardapio, AdicionarItemCardapioDto input);
    }
}
