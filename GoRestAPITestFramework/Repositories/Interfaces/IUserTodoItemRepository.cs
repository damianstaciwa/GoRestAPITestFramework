using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IUserTodoItemRepository
    {
        Task<List<TodoItem>> GetUserTodoItemsAsync(int userId);
        Task CreateUserTodoItemAsync(int userId, TodoItem newTodo);
        Task UpdateUserTodoItemAsync(int userId, int todoId, TodoItem todo);
        Task UpdateUserTodoItemPartiallyAsync(int userId, int todoId, object partialUpdate);
        Task DeleteUserTodoItemAsync(int userId, int todoId);
    }
}