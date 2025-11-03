namespace Vinder.Comanda.Profiles.Application.Payloads.Customer;

public sealed record CustomerDeletionScheme : IMessage<Result>
{
    [property: JsonIgnore]
    public string CustomerId { get; init; } = default!;
}