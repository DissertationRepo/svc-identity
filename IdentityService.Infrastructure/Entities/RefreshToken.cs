using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public string TokenHash { get; init; }

        public DateTime ExpiresAt { get; init; }

        public bool Revoked { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime? RevokedAt { get; init; }
        public Guid? ReplacedByTokenId { get; set; }
        public User User { get; init; }
    }
}
