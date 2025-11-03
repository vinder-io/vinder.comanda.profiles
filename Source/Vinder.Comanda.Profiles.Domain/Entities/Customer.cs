namespace Vinder.Comanda.Profiles.Domain.Entities;

public sealed class Customer : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public User User { get; set; } = default!;
    public Contact Contact { get; set; } = default!;
    public ICollection<Address> Addresses = [  ];
}