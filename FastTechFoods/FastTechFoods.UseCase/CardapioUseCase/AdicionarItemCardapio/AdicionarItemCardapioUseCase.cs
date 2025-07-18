using Domain.Interfaces;
using FluentValidation;
using UseCase.CardapioUseCase.Shared;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.AdicionarItemCardapio
{
    public class AdicionarItemCardapioUseCase : IAdicionarItemCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;
        private readonly IValidator<AdicionarItemCardapioDto> _validator;
        private readonly IMessagePublisher _publisherCardapio;

        public AdicionarItemCardapioUseCase(ICardapioRepository cardapioRepository, IValidator<AdicionarItemCardapioDto> validator, Func<string, IMessagePublisher> publisherFactory)
        {
            _cardapioRepository = cardapioRepository;
            _validator = validator;
            _publisherCardapio = publisherFactory("ProducerCardapio");
        }

        public async Task<ItemAdicionadoDto> Adicionar(Guid idRestaurante, Guid idCardapio, AdicionarItemCardapioDto adicionarItemDto)
        {
            var validacao = _validator.Validate(adicionarItemDto);
            if (!validacao.IsValid)
            {
                string mensagemValidacao = string.Empty;
                foreach (var item in validacao.Errors)
                {
                    mensagemValidacao = string.Concat(mensagemValidacao, item.ErrorMessage, ", ");
                }

                throw new Exception(mensagemValidacao.Remove(mensagemValidacao.Length - 2));
            }

            var cardapio = _cardapioRepository.ObterPorId(idCardapio);

            if (cardapio is null || cardapio.RestauranteId != idRestaurante)
            {
                throw new Exception("Cardapio não encontrado");
            }

            var itemDeCardapio = cardapio.AdicionarItem(idCardapio, adicionarItemDto.Nome, adicionarItemDto.Valor, adicionarItemDto.Descricao, adicionarItemDto.Tipo);

            _cardapioRepository.Atualizar(cardapio);

            await _publisherCardapio.PublishAsync(new CardapioAtualizadoDto
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

            return new ItemAdicionadoDto
            {
                Id = itemDeCardapio.Id,
            };
        }
    }
}
