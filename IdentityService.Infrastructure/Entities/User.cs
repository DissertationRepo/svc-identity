namespace IdentityService.Infrastructure.Entities
{
    public class User
    {
        public Guid Id { get; init; } 
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public string Role { get; init; }
        public DateTime CreatedAt { get; init; }
        public ICollection<RefreshToken> RefreshTokens { get; init; } = new List<RefreshToken>();
    }
}
