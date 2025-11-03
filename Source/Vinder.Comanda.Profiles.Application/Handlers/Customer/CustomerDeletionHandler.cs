namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class CustomerDeletionHandler(ICustomerRepository repository) :
    IMessageHandler<CustomerDeletionScheme, Result>
{
    public async Task<Result> HandleAsync(CustomerDeletionScheme message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
             .WithIdentifier(message.CustomerId)
             .WithIsDeleted(false)
             .Build();

        var matchingCustomers = await repository.GetCustomersAsync(filters, cancellation);
        var existingCustomer = matchingCustomers.FirstOrDefault();

        if (existingCustomer is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-AF04C */
            return Result.Failure(CustomerErrors.CustomerDoesNotExist);
        }

        await repository.DeleteAsync(existingCustomer, cancellation);

        return Result.Success();
    }
}