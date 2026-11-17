using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configuration;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(p => p.Total, total =>
        {
            total.Property(d => d.Valor)
                .HasColumnName("Total")
                .HasPrecision(18, 2);
        });

        builder.Navigation(p => p.Total)
            .IsRequired();

        builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(p => p.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(p => p.ItemPedidos)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}