using Domain.Interfaces;
using FluentValidation;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.AdicionarItemCardapio
{
    public class AdicionarItemCardapioUseCase : IAdicionarItemCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;
        private readonly IValidator<AdicionarItemCardapioDto> _validator;

        public AdicionarItemCardapioUseCase(ICardapioRepository cardapioRepository, IValidator<AdicionarItemCardapioDto> validator)
        {
            _cardapioRepository = cardapioRepository;
            _validator = validator;
        }

        public ItemAdicionadoDto Adicionar(Guid idRestaurante, Guid idCardapio, AdicionarItemCardapioDto adicionarItemDto)
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

            return new ItemAdicionadoDto
            {
                Id = itemDeCardapio.Id,
            };
        }
    }
}
