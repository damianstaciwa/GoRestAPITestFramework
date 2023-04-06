using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync();
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(int postId, Post post);
        Task UpdatePostPartiallyAsync(int postId, object partialUpdate);
        Task DeletePostAsync(int postId);
    }
}