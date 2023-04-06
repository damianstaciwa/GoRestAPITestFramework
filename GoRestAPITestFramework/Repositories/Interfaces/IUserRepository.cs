using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(int userId, User user);
        Task UpdateUserPartiallyAsync(int userId, object partialUpdate);
        Task DeleteUserAsync(int userId);
    }
}