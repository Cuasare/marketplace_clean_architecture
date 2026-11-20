namespace Marketplace.Application.Interfaces;

public interface IPasswordHasher
{
    string HashearSenha(string senha);
    bool VerificarSenha(string senha, string senhaHash);
}