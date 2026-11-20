using Marketplace.Domain.Exceptions;

namespace Marketplace.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiraEm {get; private set;}
    public DateTime CriadoEm { get; private set; }
    public DateTime? RevogadoEm { get; private set; }
    public bool LembrarDeMim { get; private set; }
    public Guid UsuarioId { get; private set; }

    private RefreshToken(){}

    public RefreshToken(string tokenHash, DateTime expiraEm, Guid usuarioId, bool lembrarDeMim)
    {
        if (usuarioId == Guid.Empty)
            throw new DomainException("Id do usuário não pode estar vazio");

        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new DomainException("Token não pode ser vazio");

        if (expiraEm <= DateTime.UtcNow)
            throw new DomainException("A data precisa ser futura");

        Id = Guid.CreateVersion7();
        TokenHash = tokenHash;
        ExpiraEm = expiraEm;
        CriadoEm = DateTime.UtcNow;
        LembrarDeMim = lembrarDeMim;
        UsuarioId = usuarioId;
    }

    public bool EstaExpirado()
    {
        return ExpiraEm <= DateTime.UtcNow;
    }

    public bool EstaRevogado()
    {
        return RevogadoEm.HasValue;
    }

    public bool EstaAtivo()
    {
        return !EstaExpirado() && !EstaRevogado();
    }

    public void Revogar()
    {
        RevogadoEm = DateTime.UtcNow;
    }
}
