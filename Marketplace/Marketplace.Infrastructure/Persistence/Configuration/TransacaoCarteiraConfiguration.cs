using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configuration;

public class TransacaoCarteiraConfiguration : IEntityTypeConfiguration<TransacaoCarteira>
{
    public void Configure(EntityTypeBuilder<TransacaoCarteira> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasOne<Carteira>()
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.CarteiraId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(t => t.Valor, valor =>
        {
            valor.Property(d => d.Valor)
                .HasColumnName("Valor")
                .HasPrecision(18, 2);
        });

        builder.Property(t => t.TipoTransacao)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Navigation(t => t.Valor)
            .IsRequired();

        builder.Property(t => t.Id)
            .ValueGeneratedNever();
    }
}