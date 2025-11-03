namespace Vinder.Comanda.Profiles.Application.Mappers;

public static class OwnerMapper
{
    public static Owner AsOwner(this OwnerCreationScheme owner)
    {
        var user = new User(owner.UserId, owner.Username);
        var contact = new Contact(owner.Email, owner.PhoneNumber);

        return new Owner
        {
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            User = user,
            Contact = contact
        };
    }

    public static Owner AsOwner(this EditOwnerScheme scheme, Owner existingOwner)
    {
        existingOwner.FirstName = scheme.FirstName;
        existingOwner.LastName = scheme.LastName;

        existingOwner.Contact = new Contact(
            Email: scheme.Email,
            PhoneNumber: scheme.PhoneNumber
        );

        return existingOwner;
    }


    public static OwnerScheme AsResponse(this Owner owner) => new()
    {
        Identifier = owner.Id,

        FirstName = owner.FirstName,
        LastName = owner.LastName,

        Email = owner.Contact.Email,
        PhoneNumber = owner.Contact.PhoneNumber,

        UserId = owner.User.Identifier,
        Username = owner.User.Username
    };
}