using AutoMapper;
using IdentityService.Infrastructure.Entities;

namespace IdentityService.Infrastructure.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, Domain.Entities.User>()
                .ConstructUsing(src => CreateDomainUser(src));
        }

        private Domain.Entities.User CreateDomainUser(User src)
        {
            return new Domain.Entities.User(
                src.FirstName,
                src.LastName,
                src.Email,
                src.PasswordHash,
                src.Role
            );
        }
    }
}
