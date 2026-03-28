using AutoMapper;
using IdentityService.Application.AbstractServices;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _db;
        private readonly IMapper _mapper;

        public RefreshTokenRepository(IdentityDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                var infraRefreshToken = _mapper.Map<Infrastructure.Entities.RefreshToken>(refreshToken);
                await _db.RefreshTokens.AddAsync(infraRefreshToken);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        { 
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenHash == token);

            var domainRefreshToken = _mapper.Map<RefreshToken>(refreshToken);

            return domainRefreshToken;
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var infraRefreshToken = _mapper.Map<Infrastructure.Entities.RefreshToken>(refreshToken);
            _db.RefreshTokens.Update(infraRefreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<Domain.Entities.RefreshToken?> GetByTokenHashAsync(string tokenHash)
        {
            var infraToken = await _db.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

            if (infraToken is null)
            {
                return null;
            }

            return _mapper.Map<Domain.Entities.RefreshToken>(infraToken);
        }

        public async Task UpdateOldTokenAsync(Guid userId, Guid newRefreshTokenId)
        {
            var oldToken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.ReplacedByTokenId == null);
            if ( oldToken != null )
            {
                oldToken.ReplacedByTokenId = newRefreshTokenId;
            }
            await _db.SaveChangesAsync();
        }
    }
}
