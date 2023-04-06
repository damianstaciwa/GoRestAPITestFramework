using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly RestClient _client;

        public PostRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var request = new RestRequest("posts", Method.Get);
            var response = await _client.ExecuteAsync<List<Post>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<Post>();
            }
        }

        public async Task CreatePostAsync(Post post)
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddJsonBody(post);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdatePostAsync(int postId, Post post)
        {
            var request = new RestRequest($"posts/{postId}", Method.Put);
            request.AddJsonBody(post);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdatePostPartiallyAsync(int postId, object partialUpdate)
        {
            var request = new RestRequest($"posts/{postId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task DeletePostAsync(int postId)
        {
            var request = new RestRequest($"posts/{postId}", Method.Delete);
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