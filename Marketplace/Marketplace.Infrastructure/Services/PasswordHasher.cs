using Marketplace.Application.Interfaces;

namespace Marketplace.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashearSenha(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha);

    public bool VerificarSenha(string senha, string senhaHash)
        => BCrypt.Net.BCrypt.Verify(senha, senhaHash);
}