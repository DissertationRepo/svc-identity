using AutoMapper;

namespace IdentityService.Api.Mapping
{
    public class LogoutMapping : Profile
    {
        public LogoutMapping() 
        {
            CreateMap<Models.Logout, Application.Models.Logout>();
        }
    }
}
