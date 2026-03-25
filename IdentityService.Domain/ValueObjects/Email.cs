namespace IdentityService.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        var normalized = email.Trim().ToLowerInvariant();

        if (!normalized.Contains("@")) throw new ArgumentException("Invalid email format.", nameof(email));
        return new Email(normalized);
    }
    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is Email other && Value == other.Value;
}
