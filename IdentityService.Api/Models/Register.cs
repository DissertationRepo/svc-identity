namespace IdentityService.Api.Models
{
    public record Register
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? Role { get; init; }
        public string? ClientId { get; init; }
    }
}
