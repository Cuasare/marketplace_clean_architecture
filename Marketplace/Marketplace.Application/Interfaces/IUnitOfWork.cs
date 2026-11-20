namespace Marketplace.Application.Interfaces;

public interface IUnitOfWork
{
    Task SalvarAsync(CancellationToken ct = default);
}