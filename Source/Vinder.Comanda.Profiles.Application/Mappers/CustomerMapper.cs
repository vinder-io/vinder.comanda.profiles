namespace Vinder.Comanda.Profiles.Application.Mappers;

public static class CustomerMapper
{
    public static Customer AsCustomer(this CustomerCreationScheme customer) => new()
    {
        FirstName = customer.FirstName,
        LastName = customer.LastName,

        User = new User(customer.UserId, customer.Username),
        Contact = new Contact(customer.Email, customer.PhoneNumber)
    };

    public static Customer AsCustomer(this EditCustomerScheme scheme, Customer existingCustomer)
    {
        existingCustomer.FirstName = scheme.FirstName;
        existingCustomer.LastName = scheme.LastName;

        existingCustomer.Contact = new Contact(
            Email: scheme.Email,
            PhoneNumber: scheme.PhoneNumber
        );

        return existingCustomer;
    }

    public static CustomerScheme AsResponse(this Customer customer) => new()
    {
        Identifier = customer.Id,

        FirstName = customer.FirstName,
        LastName = customer.LastName,

        Email = customer.Contact.Email,
        PhoneNumber = customer.Contact.PhoneNumber,

        UserId = customer.User.Identifier,
        Username = customer.User.Username
    };
}