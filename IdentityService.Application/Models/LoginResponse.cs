using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Models
{
    public record LoginResponse
    {
        public string RefreshToken { get; init; } = string.Empty;
        public string AccessToken { get; init; } = string.Empty;
        public Guid UserId { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}
