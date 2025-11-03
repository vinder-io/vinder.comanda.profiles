namespace Vinder.Comanda.Profiles.Domain.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<IReadOnlyCollection<Customer>> GetCustomersAsync(
        CustomerFilters filters,
        CancellationToken cancellation = default
    );

    public Task<long> CountCustomersAsync(
        CustomerFilters filters,
        CancellationToken cancellation = default
    );
}