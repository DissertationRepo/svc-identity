namespace IdentityService.Application.Models
{
    public record RegisterResponse
    {
        public Guid UserId { get; init; }
        public string Role { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
