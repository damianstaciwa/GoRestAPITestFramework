using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly RestClient _client;

        public TodoItemRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            var request = new RestRequest("todos", Method.Get);
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

        public async Task CreateTodoItemAsync(TodoItem todo)
        {
            var request = new RestRequest("todos", Method.Post);
            request.AddJsonBody(todo);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdateTodoItemAsync(int todoId, TodoItem todo)
        {
            var request = new RestRequest($"todos/{todoId}", Method.Put);
            request.AddJsonBody(todo);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task UpdateTodoItemPartiallyAsync(int todoId, object partialUpdate)
        {
            var request = new RestRequest($"todos/{todoId}", Method.Patch);
            request.AddJsonBody(partialUpdate);
            var response = await _client.ExecuteAsync(request);

            HandleResponse(response);
        }

        public async Task DeleteTodoItemAsync(int todoId)
        {
            var request = new RestRequest($"todos/{todoId}", Method.Delete);
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