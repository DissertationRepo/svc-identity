namespace IdentityService.Application.AbstractServices
{
    public interface IRefreshTokenGenerator
    {
        string GenerateRefreshToken();
    }
}
