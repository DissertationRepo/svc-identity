using System.ComponentModel.DataAnnotations;

namespace IdentityService.Api.Models
{
    public class Login
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? ClientId { get; init; }
    }
}
