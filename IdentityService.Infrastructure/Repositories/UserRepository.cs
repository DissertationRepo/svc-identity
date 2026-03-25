using IdentityService.Application.AbstractServices;
using DomainUser = IdentityService.Domain.Entities.User;
using IdentityService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(IdentityDbContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DomainUser?> GetUserByEmailAsync(string email)
        {
            var infraUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (infraUser == null)
                return null;
            
            var domainUser = _mapper.Map<DomainUser>(infraUser);
            return domainUser;
        }

        public async Task<bool> AddUserAsync(DomainUser user)
        {
            var infraUser = _mapper.Map<User>(user);
            try
            {
                await _db.Users.AddAsync(infraUser);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
