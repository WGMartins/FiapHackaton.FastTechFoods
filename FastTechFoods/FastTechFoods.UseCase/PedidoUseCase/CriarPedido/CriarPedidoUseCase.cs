using Domain.Interfaces;
using Domain.PedidoAggregate;
using FluentValidation;
using UseCase.Interfaces;
using UseCase.PedidoUseCase.Shared;

namespace UseCase.PedidoUseCase.CriarPedido;

public class CriarPedidoUseCase : ICriarPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ICardapioRepository _cardapioRepository;
    private readonly IValidator<AdicionarPedidoDto> _validator;

    public CriarPedidoUseCase(IPedidoRepository pedidoRepository, ICardapioRepository cardapioRepository, IValidator<AdicionarPedidoDto> validator)
    {
        _pedidoRepository = pedidoRepository;
        _cardapioRepository = cardapioRepository;
        _validator = validator;
    }

    public PedidoAdicionadoDto Criar(Guid idCliente, AdicionarPedidoDto adicionarPedidoDto)
    {
        var validacao = _validator.Validate(adicionarPedidoDto);
        if (!validacao.IsValid)
        {
            string mensagemValidacao = string.Empty;
            foreach (var item in validacao.Errors)
            {
                mensagemValidacao = string.Concat(mensagemValidacao, item.ErrorMessage, ", ");
            }

            throw new Exception(mensagemValidacao.Remove(mensagemValidacao.Length - 2));
        }

        Pedido pedido = Pedido.Criar(adicionarPedidoDto.RestauranteId, idCliente, adicionarPedidoDto.FormaDeEntrega);

        foreach (var itempedido in adicionarPedidoDto.ItensDePedido)
        {
            var itemDeCardapio = _cardapioRepository.ObterItemPorId(itempedido.ItemDoCardapioId);

            if (itemDeCardapio is null || itemDeCardapio.Cardapio.RestauranteId != pedido.RestauranteId)
            {
                throw new Exception("Item não encontrado");
            }

            pedido.AdicionarItem(pedido.Id, itemDeCardapio.Nome, itemDeCardapio.Valor, itempedido.Quantidade);
        }

        _pedidoRepository.Adicionar(pedido);

        return new PedidoAdicionadoDto
        {
            Id = pedido.Id,
        };
    }
}
