using IdentityService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.AbstractServices
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken hashedToken);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetByTokenHashAsync(string token);
        Task UpdateOldTokenAsync(Guid userId,  Guid newRefreshTokenId);
    }
}
