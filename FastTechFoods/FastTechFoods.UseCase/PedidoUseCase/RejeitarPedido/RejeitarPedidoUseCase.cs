﻿using Domain.Interfaces;
using Domain.PedidoAggregate;
using UseCase.Interfaces;

namespace UseCase.PedidoUseCase.RejeitarPedido
{
    public class RejeitarPedidoUseCase : IRejeitarPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMessagePublisher _publisherPedido;

        public RejeitarPedidoUseCase(IPedidoRepository pedidoRepository, Func<string, IMessagePublisher> publisherFactory)
        {
            _pedidoRepository = pedidoRepository;
            _publisherPedido = publisherFactory("ProducerPedido");
        }

        public void Rejeitar(Guid idRestaurante, Guid id)
        {
            var pedido = _pedidoRepository.ObterPorId(id);

            if (pedido is null || pedido.RestauranteId != idRestaurante)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (pedido.Status != Status.Enviado)
            {
                throw new Exception("Pedido não permite rejeição");
            }

            pedido.AlterarStatus(Status.Rejeitado);

            _pedidoRepository.Atualizar(pedido);

            _publisherPedido.PublishAsync(new RejeitarPedidoDto
            {
                Id = pedido.Id,
                RestauranteId = idRestaurante,
                Status = Status.Rejeitado,
            });
        }
    }
}
