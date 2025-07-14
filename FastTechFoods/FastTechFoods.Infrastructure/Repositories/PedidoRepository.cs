using Domain.Interfaces;
using Domain.PedidoAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        protected ApplicationDbContext _context;
        protected DbSet<Pedido> _dbSet;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Pedido>();
        }

        public void Adicionar(Pedido pedido)
        {
            _dbSet.Add(pedido);
            _context.SaveChanges();
        }

        public void Atualizar(Pedido pedido)
        {
            pedido.AlteradoEm = DateTime.Now;

            _context.SaveChanges();
        }

        public Pedido ObterPorId(Guid id)
        {
            return _dbSet.Include(x => x.ItensDePedido).FirstOrDefault(x => x.Id == id);
        }

        public IList<Pedido> Listar(Guid restauranteid)
        {
            return _dbSet.Include(x => x.Cliente).Include(x => x.ItensDePedido).Where(x => x.RestauranteId == restauranteid).ToList();
        }
    }
}
