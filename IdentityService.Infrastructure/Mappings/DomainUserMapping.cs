using AutoMapper;

namespace IdentityService.Infrastructure.Mappings
{
    public class DomainUserMapping : Profile
    {
        public DomainUserMapping() 
        {
            CreateMap<Domain.Entities.User, Infrastructure.Entities.User>()
                .ConstructUsing(src => CreateInfrastructureUser(src));
        }
        private Infrastructure.Entities.User CreateInfrastructureUser(Domain.Entities.User src)
        {
            var userInfrastructure = new Infrastructure.Entities.User
            {
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                Email = src.Email.ToString(),
                PasswordHash = src.PasswordHash,
                Role = src.Role
            };
            return userInfrastructure;
        }
    }
}
