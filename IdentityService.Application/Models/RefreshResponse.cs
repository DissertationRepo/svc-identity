namespace IdentityService.Application.Models
{
    public record RefreshResponse
    {
        public string AccessToken { get; init; }
    }
}
