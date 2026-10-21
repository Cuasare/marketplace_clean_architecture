using Marketplace.Domain.Enums;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public Guid CompradorId { get; private set; }
    public StatusPedido Status { get; private set; }
    public Dinheiro Total { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    
    
    private Pedido() {}

    private Pedido(Guid compradorId)
    {
        if (compradorId == Guid.Empty)
            throw new DomainException("Comprador é obrigatório!");

        Id = Guid.CreateVersion7();
        CompradorId = compradorId;
        Status = StatusPedido.Pago;
        Total = Dinheiro.Zero;
        DataCriacao = DateTime.UtcNow;
    }

    private readonly List<ItemPedido> _itemPedidos = new();
    public IReadOnlyCollection<ItemPedido> ItemPedidos => _itemPedidos.AsReadOnly();
    
    public static Pedido Criar(Guid compradorId, Produto produto, int quantidade)
    {
        var pedido = new Pedido(compradorId);
        pedido.AdicionarItem(produto, quantidade);
        return pedido;
    }

    private void AdicionarItem(Produto produto, int quantidade)
    {
        produto.RegistrarVenda(CompradorId, quantidade);
        var itemPedido = new ItemPedido(Id, produto.Id, quantidade, produto.PrecoAtual());
        _itemPedidos.Add(itemPedido);
        Total += itemPedido.Subtotal;
    }

    public void MarcarComoEnviado()
    {
        if (Status is not StatusPedido.Pago)
            throw new DomainException("Pedido deve estar como pago para ser enviado");
        Status = StatusPedido.Enviado;
    }

    public void MarcarComoEntregue()
    {
        if (Status is not StatusPedido.Enviado)
            throw new DomainException("Pedido deve ser enviado antes");
        Status = StatusPedido.Entregue;
    }

    public void Cancelar()
    {
        if (Status is not StatusPedido.Pago)
            throw new DomainException("Só pode ser cancelado antes de ser enviado");
        Status = StatusPedido.Cancelado;
    }
    
    
}