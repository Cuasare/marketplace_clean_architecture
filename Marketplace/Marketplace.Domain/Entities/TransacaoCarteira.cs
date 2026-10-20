using Marketplace.Domain.Enums;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class TransacaoCarteira
{
    public Guid Id { get; private set; }
    public Guid CarteiraId { get; private set; }
    public TipoTransacao TipoTransacao { get; private set; }
    public Dinheiro Valor { get; private set; } = null!;
    public DateTime DataTransacao { get; private set; }
    
    private TransacaoCarteira() {}

    internal TransacaoCarteira(Guid carteiraId, TipoTransacao tipoTransacao, Dinheiro valor)
    {
        Id = Guid.CreateVersion7();
        CarteiraId = carteiraId;
        TipoTransacao = tipoTransacao;
        Valor = valor;
    }
}