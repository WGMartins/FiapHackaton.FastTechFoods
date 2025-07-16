using Domain.Interfaces;
using FluentValidation;
using UseCase.CardapioUseCase.Shared;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.RemoverItemCardapio
{
    public class RemoverItemCardapioUseCase : IRemoverItemCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;
        private readonly IMessagePublisher _publisherCardapio;

        public RemoverItemCardapioUseCase(ICardapioRepository cardapioRepository, Func<string, IMessagePublisher> publisherFactory)
        {
            _cardapioRepository = cardapioRepository;
            _publisherCardapio = publisherFactory("ProducerCardapio");
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

            _publisherCardapio.PublishAsync(new CardapioAtualizadoDto
            {

                Id = idCardapio,
                RestauranteId = idRestaurante,
                ItensDeCardapio = cardapio.ItensDeCardapio
                .Select(i => new ItemDeCardapioAtualizadoDto
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Valor = i.Valor,
                    Descricao = i.Descricao,
                    Tipo = i.Tipo,
                }).ToList()
            });
        }
    }
}
