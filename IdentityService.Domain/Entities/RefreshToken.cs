namespace IdentityService.Domain.Entities;

public sealed class RefreshToken
{
    private RefreshToken() { } // for ORM

    public RefreshToken(string tokenHash, DateTime createdAt, DateTime expiresAt, Guid userId)
    {
        TokenHash = tokenHash ?? throw new ArgumentNullException(nameof(tokenHash));
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
        RevokedAt = null;
        UserId = userId;
        ReplacedByTokenId = null;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string TokenHash { get; private set; } = string.Empty; 
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public Guid UserId { get; private set; }

    public DateTime? RevokedAt { get; private set; }
    public Guid? ReplacedByTokenId { get; private set; }

    public bool IsExpired(DateTime now) => ExpiresAt <= now;
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsActive(DateTime now) => !IsRevoked && !IsExpired(now);

    // Domain operation: revoke token
    public void Revoke(DateTime when, string reason, Guid? replacedBy = null)
    {
        if (IsRevoked) return;
        RevokedAt = when;
        ReplacedByTokenId = replacedBy;
    }

    // Domain operation: rotate (issue a new token replacing this one)
    public void MarkReplaced(Guid newTokenId, DateTime when)
    {
        ReplacedByTokenId = newTokenId;
        RevokedAt = when;
    }
}