using Marketplace.Domain.Exceptions;

namespace Marketplace.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; private set; } = null!;
    public string SenhaHash { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }
    
    private Usuario() { }

    public Usuario(string nome, string email, string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("Nome é obrigatório!");
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new DomainException("Email é obrigatório!");
        }

        if (string.IsNullOrWhiteSpace(senhaHash))
        {
            throw new DomainException("Senha é obrigatória!");
        }

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
        SenhaHash = senhaHash;
        DataCriacao = DateTime.UtcNow;
    }

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
        {
            throw new DomainException("Nome é obrigatório");
        }

        Nome = novoNome.Trim();
    }
}