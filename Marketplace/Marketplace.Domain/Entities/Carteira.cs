using Marketplace.Domain.Enums;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Carteira
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Dinheiro Saldo { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }

    private Carteira() {}

    
    public Carteira(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
            throw new DomainException("Usuário é obrigatório");

        Id = Guid.CreateVersion7();
        UsuarioId = usuarioId;
        DataCriacao = DateTime.UtcNow;
        Saldo = Dinheiro.Zero;
    }
    
    private readonly List<TransacaoCarteira> _transacoes = new();
    public IReadOnlyCollection<TransacaoCarteira> Transacoes => _transacoes.AsReadOnly();

    public void Depositar(Dinheiro valor, TipoTransacao tipoTransacao)
    {
        if (tipoTransacao is not (TipoTransacao.Deposito or TipoTransacao.Venda or TipoTransacao.Estorno))
            throw new DomainException("Tipo de transação inválida para deposito");
        
        ValidarValor(valor);
        Saldo += valor;
        _transacoes.Add(new TransacaoCarteira(Id, tipoTransacao, valor));
    }
    
    public void Debitar(Dinheiro valor, TipoTransacao tipoTransacao)
    {
        if (tipoTransacao is not (TipoTransacao.Compra))
            throw new DomainException("Tipo de transação inválida para depósito");
        ValidarValor(valor);

        if (valor > Saldo)
            throw new DomainException("Saldo insuficiente");

        Saldo -= valor;
        _transacoes.Add(new TransacaoCarteira(Id, tipoTransacao, valor));
    }

    public static void ValidarValor(Dinheiro valor)
    {
        if (valor is null || valor == Dinheiro.Zero)
            throw new DomainException("O valor deve ser maior que zero");
    }
}