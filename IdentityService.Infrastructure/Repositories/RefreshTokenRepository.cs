using AutoMapper;
using IdentityService.Application.AbstractServices;
using IdentityService.Domain.Entities;
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
    }
}
