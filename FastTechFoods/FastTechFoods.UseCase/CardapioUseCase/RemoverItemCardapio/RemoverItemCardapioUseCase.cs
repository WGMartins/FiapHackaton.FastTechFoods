using Domain.Interfaces;
using FluentValidation;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.RemoverItemCardapio
{
    public class RemoverItemCardapioUseCase : IRemoverItemCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;

        public RemoverItemCardapioUseCase(ICardapioRepository cardapioRepository)
        {
            _cardapioRepository = cardapioRepository;
        }

        public void Remover(Guid idRestaurante, Guid idCardapio, Guid id)
        {
            var cardapio = _cardapioRepository.ObterPorId(idCardapio);

            if (cardapio is null || cardapio.RestauranteId != idRestaurante)
            {
                throw new Exception("Cardapio não encontrado");
            }

            var itemDeCardapio = cardapio.ItensDeCardapio.Where(x => x.Id == id).FirstOrDefault();

            if (itemDeCardapio is null)
            {
                throw new Exception("Item não encontrado");
            }

            cardapio.RemoverItem(itemDeCardapio);

            _cardapioRepository.Atualizar(cardapio);
        }
    }
}
