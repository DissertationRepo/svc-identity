using IdentityService.Domain.Entities;

namespace IdentityService.Application.AbstractServices
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
