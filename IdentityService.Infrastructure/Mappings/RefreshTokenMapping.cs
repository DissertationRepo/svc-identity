using System;
using AutoMapper;

namespace IdentityService.Infrastructure.Mappings
{
    public class RefreshTokenMapping : Profile
    {
        public RefreshTokenMapping() 
        {
            CreateMap<Domain.Entities.RefreshToken, Infrastructure.Entities.RefreshToken>()
                .ConstructUsing(src => CreateInfrastructureRefreshToken(src));

            CreateMap<Infrastructure.Entities.RefreshToken, Domain.Entities.RefreshToken>()
                .ConstructUsing(src => new Domain.Entities.RefreshToken(src.TokenHash, src.CreatedAt, src.ExpiresAt, src.UserId)
                {
                    Id = src.Id
                })
                .AfterMap((src, dest) =>
                {
                    if (src.ReplacedByTokenId.HasValue)
                    {
                        var when = src.RevokedAt ?? src.CreatedAt;
                        dest.MarkReplaced(src.ReplacedByTokenId.Value, when);
                        return;
                    }

                    if (src.Revoked)
                    {
                        var when = src.RevokedAt ?? DateTime.UtcNow;
                        dest.Revoke(when, "mapped-from-infrastructure");
                    }
                });
        }

        private Infrastructure.Entities.RefreshToken CreateInfrastructureRefreshToken(Domain.Entities.RefreshToken src)
        {
            return new Infrastructure.Entities.RefreshToken
            {
                Id = src.Id,
                CreatedAt = src.CreatedAt,
                UserId = src.UserId,
                Revoked = src.IsRevoked,
                ReplacedByTokenId = src.ReplacedByTokenId,
                RevokedAt = src.RevokedAt,
                TokenHash = src.TokenHash,
                ExpiresAt = src.ExpiresAt
            };
        }
    }
}
