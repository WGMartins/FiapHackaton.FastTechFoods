using Domain.UsuarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CriadoEm).HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(e => e.AlteradoEm).HasColumnType("timestamp without time zone");
            builder.Property(e => e.Email).HasMaxLength(200);
            builder.Property(e => e.Cpf).HasMaxLength(11);
            builder.Property(e => e.SenhaHash).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Role).IsRequired().HasMaxLength(50);            
        }
    }
}
