using AutoMapper;

namespace IdentityService.Api.Mapping
{
    public class RefreshMapping : Profile
    {
        public RefreshMapping() 
        {
            CreateMap<Api.Models.Refresh, Application.Models.Refresh>();
        }
    }
}
