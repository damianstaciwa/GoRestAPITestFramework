using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IUserPostsRepository
    {
        Task<List<Post>> GetUserPostsAsync(int userId);
        Task CreateUserPostAsync(int userId, Post newPost);
        Task UpdateUserPostAsync(int userId, int postId, Post post);
        Task UpdateUserPostPartiallyAsync(int userId, int postId, object partialUpdate);
        Task DeleteUserPostAsync(int userId, int postId);
    }
}