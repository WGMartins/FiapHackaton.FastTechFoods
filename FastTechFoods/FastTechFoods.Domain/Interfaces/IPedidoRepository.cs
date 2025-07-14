using Domain.PedidoAggregate;

namespace Domain.Interfaces
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        Pedido ObterPorId(Guid id);
        IList<Pedido> Listar(Guid restauranteid);
    }
}
