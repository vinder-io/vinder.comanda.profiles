namespace Vinder.Comanda.Profiles.Application.Handlers.Owner;

public sealed class FetchOwnersHandler(IOwnerRepository repository) :
    IMessageHandler<FetchOwnersParameters, Result<PaginationScheme<OwnerScheme>>>
{
    public async Task<Result<PaginationScheme<OwnerScheme>>> HandleAsync(
        FetchOwnersParameters message, CancellationToken cancellation = default)
    {
        var filters = OwnerFilters.WithSpecifications()
            .WithIdentifier(message.OwnerId)
            .WithUserId(message.UserId)
            .WithEmail(message.Email)
            .WithPhoneNumber(message.PhoneNumber)
            .WithIsDeleted(message.IsDeleted)
            .WithPagination(message.Pagination)
            .WithSort(message.Sort)
            .WithCreatedAfter(message.CreatedAfter)
            .WithCreatedBefore(message.CreatedBefore)
            .Build();

        var owners = await repository.GetOwnersAsync(filters, cancellation);
        var totalCount = await repository.CountOwnersAsync(filters, cancellation);

        var pagination = new PaginationScheme<OwnerScheme>
        {
            Items = [.. owners.Select(owner => OwnerMapper.AsResponse(owner))],
            Total = (int)totalCount,

            PageSize = message.Pagination?.PageSize ?? 0,
            PageNumber = message.Pagination?.PageNumber ?? 0
        };

        return Result<PaginationScheme<OwnerScheme>>.Success(pagination);
    }
}