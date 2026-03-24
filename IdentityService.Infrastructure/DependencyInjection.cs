using IdentityService.Application.AbstractServices;
using IdentityService.Application.Common;
using IdentityService.Application.Services;
using IdentityService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<ILoginService, LoginService>();

            // Add DbContext, repositories, external services here
            return services;
        }
    }
}