namespace IdentityService.Api.Models
{
    public record UpdateProfile
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
