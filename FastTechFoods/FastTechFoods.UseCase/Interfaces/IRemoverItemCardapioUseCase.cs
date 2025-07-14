using UseCase.CardapioUseCase.AtualizarItemCardapio;

namespace UseCase.Interfaces
{
    public interface IRemoverItemCardapioUseCase
    {
        void Remover(Guid idRestaurante, Guid idCardapio, Guid id);
    }
}
