using UseCase.CardapioUseCase.AtualizarItemCardapio;

namespace UseCase.Interfaces
{
    public interface IAtualizarItemCardapioUseCase
    {
        void Atualizar(Guid idRestaurante, Guid idCardapio, Guid id, AtualizarItemCardapioDto input);
    }
}
