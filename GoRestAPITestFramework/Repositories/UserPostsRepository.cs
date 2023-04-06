using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class UserPostRepository : IUserPostsRepository
    {
        private readonly RestClient _client;

        public UserPostRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Post>> GetUserPostsAsync(int userId)
        {
            var request = new RestRequest($"users/{userId}/posts", Method.Get);
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

        public async Task CreateUserPostAsync(int userId, Post newPost)
        {
            var request = new RestRequest($"users/{userId}/posts", Method.Post);
            request.AddJsonBody(newPost);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdateUserPostAsync(int userId, int postId, Post post)
        {
            var request = new RestRequest($"users/{userId}/posts/{postId}", Method.Put);
            request.AddJsonBody(post);
            var response = await _client.ExecuteAsync(request);
            HandleResponse(response);
        }

        public async Task UpdateUserPostPartiallyAsync(int userId, int postId, object partialUpdate)
        {
            var request = new RestRequest($"users/{userId}/posts/{postId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);
            HandleResponse(response);
        }

        public async Task DeleteUserPostAsync(int userId, int postId)
        {
            var request = new RestRequest($"users/{userId}/posts/{postId}", Method.Delete);
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