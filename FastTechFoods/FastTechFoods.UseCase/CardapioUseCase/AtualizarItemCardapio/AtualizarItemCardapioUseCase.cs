using Domain.Interfaces;
using FluentValidation;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.AtualizarItemCardapio
{
    public class AtualizarItemCardapioUseCase : IAtualizarItemCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;
        private readonly IValidator<AtualizarItemCardapioDto> _validator;

        public AtualizarItemCardapioUseCase(ICardapioRepository cardapioRepository, IValidator<AtualizarItemCardapioDto> validator)
        {
            _cardapioRepository = cardapioRepository;
            _validator = validator;
        }

        public void Atualizar(Guid idRestaurante, Guid idCardapio, Guid id, AtualizarItemCardapioDto atualizarItemDto)
        {
            var validacao = _validator.Validate(atualizarItemDto);
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

            var itemDeCardapio = cardapio.ItensDeCardapio.Where(x => x.Id == id).FirstOrDefault();

            if (itemDeCardapio is null)
            {
                throw new Exception("Item não encontrado");
            }

            cardapio.AtualizarItem(id, atualizarItemDto.Nome, atualizarItemDto.Valor, atualizarItemDto.Descricao, atualizarItemDto.Tipo);

            _cardapioRepository.Atualizar(cardapio);
        }
    }
}
