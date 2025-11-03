namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class EditCustomerHandler(ICustomerRepository repository) :
    IMessageHandler<EditCustomerScheme, Result<CustomerScheme>>
{
    public async Task<Result<CustomerScheme>> HandleAsync(
        EditCustomerScheme message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
             .WithIdentifier(message.CustomerId)
             .Build();

        var matchingCustomers = await repository.GetCustomersAsync(filters, cancellation);
        var existingCustomer = matchingCustomers.FirstOrDefault();

        if (existingCustomer is null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-AF04C */
            return Result<CustomerScheme>.Failure(CustomerErrors.CustomerDoesNotExist);
        }

        var customer = await repository.UpdateAsync(message.AsCustomer(existingCustomer), cancellation);

        return Result<CustomerScheme>.Success(customer.AsResponse());
    }
}