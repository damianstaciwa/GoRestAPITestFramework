using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetTodosAsync();
        Task CreateTodoItemAsync(TodoItem todo);
        Task UpdateTodoItemAsync(int todoId, TodoItem todo);
        Task UpdateTodoItemPartiallyAsync(int todoId, object partialUpdate);
        Task DeleteTodoItemAsync(int todoId);
    }
}