using Marketplace.Domain.Entities;

namespace Marketplace.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
    string GerarRefreshToken();
    string HashearRefreshToken(string token);
}