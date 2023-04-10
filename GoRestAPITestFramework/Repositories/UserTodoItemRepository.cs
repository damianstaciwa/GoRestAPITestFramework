using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    internal class UserTodoItemRepository : IUserTodoItemRepository
    {
        private readonly RestClient _client;

        public UserTodoItemRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<TodoItem>> GetUserTodoItemsAsync(int userId)
        {
            var request = new RestRequest($"users/{userId}/todos", Method.Get);
            var response = await _client.ExecuteAsync<List<TodoItem>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<TodoItem>();
            }
        }

        public async Task CreateUserTodoItemAsync(int userId, TodoItem newTodo)
        {
            var request = new RestRequest($"users/{userId}/todos", Method.Post);
            request.AddJsonBody(newTodo);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }
    }
}