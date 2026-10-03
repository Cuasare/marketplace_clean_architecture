using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public Guid VendedorId { get; private set; }

    public string Titulo { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public Dinheiro Preco { get; private set; } = null!;
    public Dinheiro? PrecoPromocional { get; private set; }
    public DateTime? PromocaoValidaAte { get; private set; }
    public int Estoque { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Produto() { } // EF Core

    public Produto(string titulo, string descricao, int estoque, Dinheiro preco, Guid vendedorId)
    {
        if (vendedorId == Guid.Empty)
            throw new DomainException("Vendedor é obrigatório.");

        if (estoque < 0)
            throw new DomainException("Estoque não pode ser negativo.");

        ValidarTitulo(titulo);
        ValidarDescricao(descricao);
        ValidarPreco(preco);

        Id = Guid.CreateVersion7();
        VendedorId = vendedorId;
        Titulo = titulo.Trim();
        Descricao = descricao.Trim();
        Estoque = estoque;
        Preco = preco;
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }


    public void AlterarTitulo(string titulo)
    {
        ValidarTitulo(titulo);
        Titulo = titulo.Trim();
    }

    public void AlterarDescricao(string descricao)
    {
        ValidarDescricao(descricao);
        Descricao = descricao.Trim();
    }

    public void AlterarPreco(Dinheiro preco)
    {
        ValidarPreco(preco);
        Preco = preco;

        if (PrecoPromocional is not null && PrecoPromocional >= Preco)
            RemoverPromocao();
    }

    public void Ativar() => Ativo = true;
    public void Desativar() => Ativo = false;
    

    public void AplicarPromocao(Dinheiro precoPromocional, DateTime validaAte)
    {
        if (precoPromocional == Dinheiro.Zero || precoPromocional >= Preco)
            throw new DomainException("O preço promocional deve ser maior que zero e menor que o preço normal.");

        if (validaAte <= DateTime.UtcNow)
            throw new DomainException("A validade da promoção deve ser no futuro.");

        PrecoPromocional = precoPromocional;
        PromocaoValidaAte = validaAte;
    }

    public void RemoverPromocao()
    {
        PrecoPromocional = null;
        PromocaoValidaAte = null;
    }

    public Dinheiro PrecoAtual()
        => PrecoPromocional is not null && PromocaoValidaAte > DateTime.UtcNow
            ? PrecoPromocional
            : Preco;

    public bool TemEstoque(int quantidade) => quantidade > 0 && Estoque >= quantidade;

    public void RegistrarVenda(Guid compradorId, int quantidade)
    {
        if (!Ativo)
            throw new DomainException("Produto indisponível.");

        if (compradorId == VendedorId)
            throw new DomainException("Você não pode comprar o próprio produto.");

        BaixarEstoque(quantidade);
    }

    public void BaixarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");

        if (quantidade > Estoque)
            throw new DomainException("A quantidade não pode ser maior que o estoque disponivel");

        Estoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");

        Estoque += quantidade;
    }


    private static void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("Título é obrigatório.");
    }

    private static void ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Descrição é obrigatória.");
    }

    private static void ValidarPreco(Dinheiro preco)
    {
        if (preco is null || preco == Dinheiro.Zero)
            throw new DomainException("O preço deve ser maior que zero.");
    }
}