namespace IdentityService.Api.Models
{
    public record Refresh
    {
        public string? RefreshToken { get; init; }
        public string? UserId { get; init; }
    }
}
