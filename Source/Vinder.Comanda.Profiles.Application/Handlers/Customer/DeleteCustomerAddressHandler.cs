namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class DeleteCustomerAddressHandler(ICustomerRepository repository) :
    IMessageHandler<DeleteCustomerAddressScheme, Result>
{
    public async Task<Result> HandleAsync(
        DeleteCustomerAddressScheme message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
             .WithIdentifier(message.CustomerId)
             .Build();

        var customers = await repository.GetCustomersAsync(filters, cancellation);
        var customer = customers.FirstOrDefault();

        if (customer is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-AF04C */
            return Result.Failure(CustomerErrors.CustomerDoesNotExist);
        }

        var existingAddress = customer.Addresses.FirstOrDefault(address => address.Equals(message.Target));
        if (existingAddress is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-2616B */
            return Result.Failure(CustomerErrors.AddressDoesNotExist);
        }

        customer.Addresses.Remove(existingAddress);

        await repository.UpdateAsync(customer, cancellation);

        return Result.Success();
    }
}