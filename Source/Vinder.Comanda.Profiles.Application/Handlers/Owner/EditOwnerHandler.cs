namespace Vinder.Comanda.Profiles.Application.Handlers.Owner;

public sealed class EditOwnerHandler(IOwnerRepository repository) :
    IMessageHandler<EditOwnerScheme, Result<OwnerScheme>>
{
    public async Task<Result<OwnerScheme>> HandleAsync(
        EditOwnerScheme message, CancellationToken cancellation = default)
    {
        var filters = OwnerFilters.WithSpecifications()
             .WithIdentifier(message.OwnerId)
             .WithIsDeleted(false)
             .Build();

        var matchingOwners = await repository.GetOwnersAsync(filters, cancellation);
        var existingOwner = matchingOwners.FirstOrDefault();

        if (existingOwner is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-0831D */
            return Result<OwnerScheme>.Failure(OwnerErrors.OwnerDoesNotExist);
        }

        var owner = await repository.UpdateAsync(message.AsOwner(existingOwner), cancellation);

        return Result<OwnerScheme>.Success(owner.AsResponse());
    }
}