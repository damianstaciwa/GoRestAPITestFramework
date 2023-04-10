using GoRestAPITestFramework.Models;

namespace GoRestAPITestFramework.Repositories.Interfaces
{
    public interface IUserTodoItemRepository
    {
        Task<List<TodoItem>> GetUserTodoItemsAsync(int userId);
        Task CreateUserTodoItemAsync(int userId, TodoItem newTodo);
    }
}