using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class ItemPedido
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }
    public Dinheiro PrecoUnitario { get; private set; } = null!;

    public Dinheiro Subtotal => PrecoUnitario * Quantidade;

    private ItemPedido() { }

    internal ItemPedido(Guid pedidoId, Guid produtoId, int quantidade, Dinheiro precoUnitario)
    {
        Id = Guid.CreateVersion7();
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }
    
}