namespace Vinder.Comanda.Profiles.Application.Payloads.Customer;

public sealed record FetchCustomerAddressesParameters :
    IMessage<Result<IReadOnlyCollection<Address>>>
{
    public string CustomerId { get; init; } = default!;
}