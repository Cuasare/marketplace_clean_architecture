using Marketplace.Application.Interfaces;
using Marketplace.Domain.Entities;
using Marketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext db) : IUsuarioRepository
{
    public Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken ct = default)
        => db.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task AdicionarAsync(Usuario usuario, CancellationToken ct = default)
    {
        await db.Usuarios.AddAsync(usuario, ct);
    }

    public Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default)
        => db.Usuarios.AnyAsync(u => u.Email == email, ct);
}