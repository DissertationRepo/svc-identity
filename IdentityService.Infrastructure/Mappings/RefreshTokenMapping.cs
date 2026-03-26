using AutoMapper;

namespace IdentityService.Infrastructure.Mappings
{
    public class RefreshTokenMapping : Profile
    {
        public RefreshTokenMapping() 
        {
            CreateMap<Domain.Entities.RefreshToken, Infrastructure.Entities.RefreshToken>()
                .ConstructUsing(src => CreateInfrastructureRefreshToken(src));
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
