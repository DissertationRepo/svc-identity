namespace IdentityService.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Value { get; }

    public PasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash)) throw new ArgumentException("Hash cannot be empty.", nameof(hash));
        Value = hash;
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is PasswordHash p && Value == p.Value;
}