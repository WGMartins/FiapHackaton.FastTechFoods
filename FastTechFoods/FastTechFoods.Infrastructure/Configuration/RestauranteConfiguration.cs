using Domain.CardapioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class RestauranteConfiguration : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.Nome).IsRequired();

            builder.HasOne(r => r.Cardapio)
                .WithOne(c => c.Restaurante)
                .HasForeignKey<Restaurante>(r => r.CardapioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Pedidos)
                .WithOne(p => p.Restaurante)
                .HasForeignKey(p => p.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
