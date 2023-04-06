using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync();
        Task CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(int commentId, Comment comment);
        Task UpdateCommentPartiallyAsync(int commentId, object partialUpdate);
        Task DeleteCommentAsync(int commentId);
    }
}