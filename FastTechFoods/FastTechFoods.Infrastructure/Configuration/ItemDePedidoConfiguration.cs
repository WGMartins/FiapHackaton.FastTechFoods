using Domain.PedidoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ItemDePedidoConfiguration : IEntityTypeConfiguration<ItemDePedido>
    {
        public void Configure(EntityTypeBuilder<ItemDePedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.PedidoId).IsRequired();
            builder.Property(e => e.Nome).IsRequired();
            builder.Property(e => e.ValorUnitario).IsRequired();
            builder.Property(e => e.Quantidade).IsRequired();
            builder.Property(e => e.ValorTotal).IsRequired();

            builder.HasOne(i => i.Pedido)
                   .WithMany(c => c.ItensDePedido)
                   .HasForeignKey(i => i.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
