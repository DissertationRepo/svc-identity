namespace IdentityService.Api.Models
{
    public record Profile
    {
        public int Id { get; init; }
        public string Email { get; init; }
        public string FirstName { get;  init; }
        public string? LastName { get; init; }
        public string Roles { get; init; }
    }
}
