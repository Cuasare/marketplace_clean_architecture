using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configuration;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(i => i.PrecoUnitario, precoUnitario =>
        {
            precoUnitario.Property(d => d.Valor)
                .HasColumnName("PrecoUnitario")
                .HasPrecision(18, 2);
        });

        builder.HasOne<Produto>()
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Pedido>()
            .WithMany(p => p.ItemPedidos)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(i => i.PrecoUnitario)
            .IsRequired();

        builder.Ignore(i => i.Subtotal);
    }
}