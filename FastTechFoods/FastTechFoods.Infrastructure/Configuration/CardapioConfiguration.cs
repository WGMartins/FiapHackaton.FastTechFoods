using Domain.CardapioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CardapioConfiguration : IEntityTypeConfiguration<Cardapio>
    {
        public void Configure(EntityTypeBuilder<Cardapio> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.RestauranteId);

            builder.HasOne(c => c.Restaurante)
                .WithOne(r => r.Cardapio)
                .HasForeignKey<Cardapio>(c => c.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ItensDeCardapio)
                .WithOne(i => i.Cardapio)
                .HasForeignKey(i => i.CardapioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
