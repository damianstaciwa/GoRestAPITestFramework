using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IPostCommentsRepository
    {
        Task<List<Comment>> GetPostCommentsAsync(int postId);
        Task CreateCommentForPostAsync(int postId, Comment newComment);
    }
}