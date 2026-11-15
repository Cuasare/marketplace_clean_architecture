using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Markpetplace.Infrastructure.Persistence.Configuration;

public class CarteiraConfiguration : IEntityTypeConfiguration<Carteira>
{
    public void Configure(EntityTypeBuilder<Carteira> builder)
    {
        builder.HasOne<Usuario>()
            .WithOne()
            .HasForeignKey<Carteira>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(c => c.Saldo, saldo =>
        {
            saldo.Property(d => d.Valor)
                .HasColumnName("Saldo")
                .HasPrecision(18, 2);
        });

        builder.Navigation(c => c.Saldo)
            .IsRequired();

    }
}