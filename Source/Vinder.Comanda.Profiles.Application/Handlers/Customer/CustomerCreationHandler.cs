namespace Vinder.Comanda.Profiles.Application.Handlers.Customer;

public sealed class CustomerCreationHandler(ICustomerRepository repository, IActivityRepository activityRepository) :
    IMessageHandler<CustomerCreationScheme, Result<CustomerScheme>>
{
    public async Task<Result<CustomerScheme>> HandleAsync(
        CustomerCreationScheme message, CancellationToken cancellation = default)
    {
        var filters = CustomerFilters.WithSpecifications()
            .WithEmail(message.Email)
            .Build();

        var matchingCustomers = await repository.GetCustomersAsync(filters, cancellation);
        var existingCustomer = matchingCustomers.FirstOrDefault();

        if (existingCustomer is not null)
        {
            /* for tracking purposes: raise error #COMANDA-ERROR-76A71 */
            /* this allows us to quickly locate all occurrences of this error in the codebase. */

            return Result<CustomerScheme>.Failure(ProfileErrors.ProfileAlreadyExists);
        }

        var customer = await repository.InsertAsync(message.AsCustomer(), cancellation);
        var activity = new Activity
        {
            Action = "comanda.actions.customer.creation",
            Description = $"customer '{customer.FirstName} {customer.LastName}' created.",

            /* we can filter activities by resource using resource property */
            /* therefore, always register the resource to help us track it. */

            Resource = Resource.From(customer.Id, nameof(Customer)),
            Metadata = new Dictionary<string, string>
            {
                { "customer.identifier", customer.Id },
                { "customer.user.identifier", customer.User.Identifier }
            }
        };

        await activityRepository.InsertAsync(activity, cancellation);

        return Result<CustomerScheme>.Success(customer.AsResponse());
    }
}