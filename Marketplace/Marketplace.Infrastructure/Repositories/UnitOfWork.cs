using Marketplace.Application.Interfaces;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public async Task SalvarAsync(CancellationToken ct = default)
        => await db.SaveChangesAsync(ct);
}