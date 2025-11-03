namespace Vinder.Comanda.Profiles.Application.Payloads.Owner;

public sealed record OwnerDeletionScheme : IMessage<Result>
{
    [property: JsonIgnore]
    public string OwnerId { get; init; } = default!;
}