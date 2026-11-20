using Marketplace.Domain.Entities;

namespace Marketplace.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct = default);
    Task AdicionarAsync(Usuario usuario, CancellationToken ct = default);
    Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default);
}