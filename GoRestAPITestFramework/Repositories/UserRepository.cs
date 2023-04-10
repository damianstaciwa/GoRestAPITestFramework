using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RestClient _client;

        public UserRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var request = new RestRequest("users", Method.Get);
            var response = await _client.ExecuteAsync<List<User>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<User>();
            }
        }

        public async Task CreateUserAsync(User user)
        {
            var request = new RestRequest("users", Method.Post);
            request.AddJsonBody(user);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }

        public async Task UpdateUserAsync(int userId, User user)
        {
            var request = new RestRequest($"users/{userId}", Method.Put);
            request.AddJsonBody(user);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }

        public async Task UpdateUserPartiallyAsync(int userId, object partialUpdate)
        {
            var request = new RestRequest($"users/{userId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var request = new RestRequest($"users/{userId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }
    }
}