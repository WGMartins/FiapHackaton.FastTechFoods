using Domain.CardapioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ItemDeCardapioConfiguration : IEntityTypeConfiguration<ItemDeCardapio>
    {
        public void Configure(EntityTypeBuilder<ItemDeCardapio> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.CardapioId).IsRequired();
            builder.Property(e => e.Nome).IsRequired();
            builder.Property(e => e.Valor).IsRequired();
            builder.Property(e => e.Descricao).IsRequired();
            builder.Property(e => e.Tipo).IsRequired();

            builder.HasOne(i => i.Cardapio)
                   .WithMany(c => c.ItensDeCardapio)
                   .HasForeignKey(i => i.CardapioId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
