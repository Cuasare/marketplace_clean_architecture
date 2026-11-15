using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Markpetplace.Infrastructure.Persistence.Configuration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(p => p.VendedorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Titulo)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(300);

        builder.OwnsOne(p => p.Preco, preco =>
        {
            preco.Property(d => d.Valor)
                .HasColumnName("Preco")
                .HasPrecision(18, 2);
        });

        builder.Navigation(p => p.Preco)
            .IsRequired();

        builder.OwnsOne(p => p.PrecoPromocional, precoPromocional =>
        {
            precoPromocional.Property(d => d.Valor)
                .HasColumnName("PrecoPromocional")
                .HasPrecision(18, 2);
        });
    }
}