using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Entities;

public sealed class User
{
    private User() { } // for ORM

    public User(string firstName, string lastName, string email, string passwordHash, string role)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(FirstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(LastName));
        Email = Email.Create(email ?? throw new ArgumentNullException(nameof(Email)));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(PasswordHash));
        Role = role ?? throw new ArgumentNullException(nameof(Role))    ;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Email Email { get; init; }
    public string PasswordHash { get; init; }
    public string Role { get; init; }

}
