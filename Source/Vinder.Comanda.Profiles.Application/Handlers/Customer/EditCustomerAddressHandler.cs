namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class EditCustomerAddressHandler(ICustomerRepository repository) :
    IMessageHandler<EditCustomerAddressScheme, Result<Address>>
{
    public async Task<Result<Address>> HandleAsync(
        EditCustomerAddressScheme message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
             .WithIdentifier(message.CustomerId)
             .Build();

        var customers = await repository.GetCustomersAsync(filters, cancellation);
        var customer = customers.FirstOrDefault();

        if (customer is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-AF04C */
            return Result<Address>.Failure(CustomerErrors.CustomerDoesNotExist);
        }

        var existingAddress = customer.Addresses.FirstOrDefault(address => address.Equals(message.Target));
        if (existingAddress is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-2616B */
            return Result<Address>.Failure(CustomerErrors.AddressDoesNotExist);
        }

        if (existingAddress.Equals(message.Replacement))
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-4901F */
            return Result<Address>.Failure(CustomerErrors.AddressAlreadyAssigned);
        }

        customer.Addresses.Remove(existingAddress);
        customer.Addresses.Add(message.Replacement);

        await repository.UpdateAsync(customer, cancellation);

        return Result<Address>.Success(message.Replacement);
    }
}