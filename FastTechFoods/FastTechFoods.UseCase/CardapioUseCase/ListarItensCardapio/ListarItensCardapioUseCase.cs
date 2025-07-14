using Domain.CardapioAggregate;
using Domain.Interfaces;
using UseCase.Interfaces;

namespace UseCase.CardapioUseCase.ListarItensCardapio
{
    public class ListarItensCardapioUseCase : IListarItensCardapioUseCase
    {
        private readonly ICardapioRepository _cardapioRepository;

        public ListarItensCardapioUseCase(ICardapioRepository cardapioRepository)
        {
            _cardapioRepository = cardapioRepository;
        }

        public IList<ItemListadosDto> Listar(Guid idCliente, Guid idCardapio, string? nome, TipoRefeicao? tipoRefeicao)
        {
            var cardapio = _cardapioRepository.ObterPorId(idCardapio);

            if (cardapio is null)
            {
                throw new Exception("Cardapio não encontrado");
            }

            var itensDeCardapio = cardapio.ItensDeCardapio;

            if (nome is not null)
            {
                itensDeCardapio = itensDeCardapio.Where(x => x.Nome.Contains(nome)).ToList();
            }

            if(tipoRefeicao is not null)
            {
                itensDeCardapio = itensDeCardapio.Where(x => x.Tipo ==tipoRefeicao).ToList();
            }

            return itensDeCardapio.Select(x => Mapper(x)).ToList();
        }

        private ItemListadosDto Mapper(ItemDeCardapio itemDeCardapio)
        {
            return new ItemListadosDto
            {
                Id = itemDeCardapio.Id,
                Nome = itemDeCardapio.Nome,
                Valor = itemDeCardapio.Valor,
                Descricao = itemDeCardapio.Descricao,
                Tipo = itemDeCardapio.Tipo,
            };
        }
    }
}
