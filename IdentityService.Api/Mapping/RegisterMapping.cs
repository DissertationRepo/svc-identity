using AutoMapper;

namespace IdentityService.Api.Mapping
{
    public class RegisterMapping : Profile
    {
        public RegisterMapping()
        {
            CreateMap<Models.Register, Application.Models.Register>()
                .ConstructUsing(src => CreateApplicationRegisterProfile(src));
        }
        private Application.Models.Register CreateApplicationRegisterProfile(Models.Register src)
        {
            var registerApplication = new Application.Models.Register
            (
                src.FirstName,
                src.LastName,
                src.Email,
                src.Password,
                src.Role,
                src.ClientId
            );
            return registerApplication;
        }
    }
}
