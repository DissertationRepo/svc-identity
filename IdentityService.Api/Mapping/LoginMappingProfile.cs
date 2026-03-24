using AutoMapper;

namespace IdentityService.Api.Mapping
{
    internal class LoginMappingProfile : Profile
    {
        public LoginMappingProfile() 
        {
            CreateMap<Models.Login, Application.Models.Login>()
                .ConstructUsing(src => CreateApplicationLoginProfile(src));
        }

        private Application.Models.Login CreateApplicationLoginProfile(Models.Login src)
        {
            var loginApplication = new Application.Models.Login
            (
                src.Email,
                src.Password,
                src.ClientId
            );

            return loginApplication;
        }
    }
}
