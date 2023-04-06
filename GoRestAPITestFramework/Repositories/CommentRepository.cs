using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly RestClient _client;

        public CommentRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            var request = new RestRequest("comments", Method.Get);
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

        public async Task CreateCommentAsync(Comment comment)
        {
            var request = new RestRequest("comments", Method.Post);
            request.AddJsonBody(comment);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdateCommentAsync(int commentId, Comment comment)
        {
            var request = new RestRequest($"comments/{commentId}", Method.Put);
            request.AddJsonBody(comment);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdateCommentPartiallyAsync(int commentId, object partialUpdate)
        {
            var request = new RestRequest($"comments/{commentId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var request = new RestRequest($"comments/{commentId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        private void HandleResponse(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                throw new InvalidOperationException($"Request failed: {response.StatusCode} - {response.StatusDescription}");
            }
        }
    }
}