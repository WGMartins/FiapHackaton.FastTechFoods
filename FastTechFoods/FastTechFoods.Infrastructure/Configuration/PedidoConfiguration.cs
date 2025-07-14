using Domain.PedidoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.RestauranteId).IsRequired();
            builder.Property(e => e.ClienteId).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.FormaDeEntrega).IsRequired();
            builder.Property(e => e.ValorTotal).IsRequired();

            builder.HasOne(p => p.Restaurante)
                   .WithMany(r => r.Pedidos)
                   .HasForeignKey(p => p.RestauranteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(p => p.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ItensDePedido)
                   .WithOne(i => i.Pedido)
                   .HasForeignKey(i => i.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
