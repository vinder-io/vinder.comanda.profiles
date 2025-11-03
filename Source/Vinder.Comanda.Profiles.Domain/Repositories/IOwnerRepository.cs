namespace Vinder.Comanda.Profiles.Domain.Repositories;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    public Task<IReadOnlyCollection<Owner>> GetOwnersAsync(
        OwnerFilters filters,
        CancellationToken cancellation = default
    );

    public Task<long> CountOwnersAsync(
        OwnerFilters filters,
        CancellationToken cancellation = default
    );
}