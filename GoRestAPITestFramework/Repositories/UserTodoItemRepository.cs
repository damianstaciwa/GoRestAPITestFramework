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

            HandleResponse(response);
        }

        public async Task UpdateUserTodoItemAsync(int userId, int todoId, TodoItem todo)
        {
            var request = new RestRequest($"users/{userId}/todos/{todoId}", Method.Put);
            request.AddJsonBody(todo);
            var response = await _client.ExecuteAsync(request);
            HandleResponse(response);
        }

        public async Task UpdateUserTodoItemPartiallyAsync(int userId, int todoId, object partialUpdate)
        {
            var request = new RestRequest($"users/{userId}/todos/{todoId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);
            HandleResponse(response);
        }

        public async Task DeleteUserTodoItemAsync(int userId, int todoId)
        {
            var request = new RestRequest($"users/{userId}/todos/{todoId}", Method.Delete);
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