namespace IdentityService.Api.Models
{
    public record Logout
    {
        public string? RefreshToken { get; init; }
    }
}
