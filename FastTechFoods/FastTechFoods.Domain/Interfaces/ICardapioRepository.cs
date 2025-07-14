using Domain.CardapioAggregate;

namespace Domain.Interfaces
{
    public interface ICardapioRepository
    {
        void Atualizar(Cardapio cardapio);
        Cardapio ObterPorId(Guid id);
        ItemDeCardapio ObterItemPorId(Guid id);
    }
}
