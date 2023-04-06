using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IPostCommentsRepository
    {
        Task<List<Comment>> GetPostCommentsAsync(int postId);
        Task CreateCommentForPostAsync(int postId, Comment newComment);
        Task UpdateCommentForPostAsync(int postId, int commentId, Comment comment);
        Task UpdateCommentForPostPartiallyAsync(int postId, int commentId, object partialUpdate);
        Task DeleteCommentForPostAsync(int postId, int commentId);
    }
}