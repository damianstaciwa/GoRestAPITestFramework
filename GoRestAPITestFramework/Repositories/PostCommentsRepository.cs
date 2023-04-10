using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class PostCommentRepository : IPostCommentsRepository
    {
        private readonly RestClient _client;

        public PostCommentRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Comment>> GetPostCommentsAsync(int postId)
        {
            var request = new RestRequest($"posts/{postId}/comments", Method.Get);
            var response = await _client.ExecuteAsync<List<Comment>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<Comment>();
            }
        }

        public async Task CreateCommentForPostAsync(int postId, Comment newComment)
        {
            var request = new RestRequest($"posts/{postId}/comments", Method.Post);
            request.AddJsonBody(newComment);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }
    }
}