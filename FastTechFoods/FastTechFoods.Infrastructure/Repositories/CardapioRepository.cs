using Domain.CardapioAggregate;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CardapioRepository : ICardapioRepository
    {
        protected ApplicationDbContext _context;
        protected DbSet<Cardapio> _dbSet;

        public CardapioRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cardapio>();
        }

        public void Atualizar(Cardapio cardapio)
        {
            cardapio.AlteradoEm = DateTime.Now;

            _context.SaveChanges();
        }

        public Cardapio ObterPorId(Guid id)
        {
            return _dbSet.Include(x => x.ItensDeCardapio).FirstOrDefault(x => x.Id == id);
        }

        public ItemDeCardapio ObterItemPorId (Guid id)
        {
            return _context.Set<ItemDeCardapio>().Include(x => x.Cardapio).FirstOrDefault(x => x.Id == id);
        }
    }
}
