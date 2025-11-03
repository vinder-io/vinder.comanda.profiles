namespace Vinder.Comanda.Profiles.Application.Handlers.Owner;

public sealed class OwnerDeletionHandler(IOwnerRepository repository) :
    IMessageHandler<OwnerDeletionScheme, Result>
{
    public async Task<Result> HandleAsync(OwnerDeletionScheme message, CancellationToken cancellation = default)
    {
        var filters = OwnerFilters.WithSpecifications()
             .WithIdentifier(message.OwnerId)
             .Build();

        var matchingOwners = await repository.GetOwnersAsync(filters, cancellation);
        var existingOwner = matchingOwners.FirstOrDefault();

        if (existingOwner is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-0831D */
            return Result.Failure(OwnerErrors.OwnerDoesNotExist);
        }

        await repository.DeleteAsync(existingOwner, cancellation);

        return Result.Success();
    }
}