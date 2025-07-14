using Domain.Interfaces;
using Domain.PedidoAggregate;
using FluentValidation;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.Shared;

namespace UseCase.PedidoUseCase.AdicionarItemPedido
{
    public class AdicionarItemPedidoUseCase : IAdicionarItemPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICardapioRepository _cardapioRepository;
        private readonly IValidator<AdicionarItemPedidoDto> _validator;

        public AdicionarItemPedidoUseCase(IPedidoRepository pedidoRepository, ICardapioRepository cardapioRepository, IValidator<AdicionarItemPedidoDto> validator)
        {
            _pedidoRepository = pedidoRepository;
            _cardapioRepository = cardapioRepository;
            _validator = validator;
        }

        public ItemAdicionadoDto Adicionar(Guid idCliente, Guid id, AdicionarItemPedidoDto adicionarItemDto)
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

            var pedido = _pedidoRepository.ObterPorId(id);

            if (pedido is null || pedido.ClienteId != idCliente)
            {
                throw new Exception("Pedido não encontrado");
            }

            if(pedido.Status != Status.EmAndamento)
            {
                throw new Exception("Pedido não permite inclusão de mais itens");
            }

            var itemDeCardapio = _cardapioRepository.ObterItemPorId(adicionarItemDto.ItemDoCardapioId);

            if (itemDeCardapio is null || itemDeCardapio.Cardapio.RestauranteId != pedido.RestauranteId)
            {
                throw new Exception("Item não encontrado");
            }

            var itemDePedido = pedido.AdicionarItem(id, itemDeCardapio.Nome, itemDeCardapio.Valor, adicionarItemDto.Quantidade);

            _pedidoRepository.Atualizar(pedido);

            return new ItemAdicionadoDto
            {
                Id = itemDePedido.Id,
            };
        }
    }
}
