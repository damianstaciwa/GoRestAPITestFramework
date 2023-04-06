using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IUserPostsRepository
    {
        Task<List<Post>> GetUserPostsAsync(int userId);
        Task CreateUserPostAsync(int userId, Post newPost);
    }
}