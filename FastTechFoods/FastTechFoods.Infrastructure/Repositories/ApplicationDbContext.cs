using Domain.PedidoAggregate;
using Domain.CardapioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.UsuarioAggregate;

namespace Infrastructure.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItemDePedido> ItemDePedido { get; set; }
        public DbSet<Cardapio> Cardapio { get; set; }
        public DbSet<ItemDeCardapio> ItemDeCardapio { get; set; }
        public DbSet<Restaurante> Restaurante { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonFile(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../FastTechFoods.Api/appsettings.json"))).Build();

                optionsBuilder.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
