namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class FetchCustomersHandler(ICustomerRepository repository) :
    IMessageHandler<FetchCustomersParameters, Result<PaginationScheme<CustomerScheme>>>
{
    public async Task<Result<PaginationScheme<CustomerScheme>>> HandleAsync(
        FetchCustomersParameters message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
            .WithIdentifier(message.CustomerId)
            .WithUserId(message.UserId)
            .WithEmail(message.Email)
            .WithPhoneNumber(message.PhoneNumber)
            .WithIsDeleted(message.IsDeleted)
            .WithPagination(message.Pagination)
            .WithSort(message.Sort)
            .WithCreatedAfter(message.CreatedAfter)
            .WithCreatedBefore(message.CreatedBefore)
            .Build();

        var customers = await repository.GetCustomersAsync(filters, cancellation);
        var totalCount = await repository.CountCustomersAsync(filters, cancellation);

        var pagination = new PaginationScheme<CustomerScheme>
        {
            Items = [.. customers.Select(customer => CustomerMapper.AsResponse(customer))],
            Total = (int)totalCount,

            PageSize = message.Pagination?.PageSize ?? 20,
            PageNumber = message.Pagination?.PageNumber ?? 1
        };

        return Result<PaginationScheme<CustomerScheme>>.Success(pagination);
    }
}