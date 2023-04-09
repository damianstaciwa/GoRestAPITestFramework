using FluentAssertions;
using GoRestAPITestFramework.Models;
using NUnit.Framework;

namespace GoRestAPITestFramework
{
    [TestFixture]
    public class UserTodoItemTests
    {
        private readonly RestApiClient apiClient;

        public UserTodoItemTests()
        {
            apiClient = new RestApiClient();
        }

        [Test]
        public async Task FullUserTodoItemsCRUDTest()
        {
            var emailPrefix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            var user = new User
            {
                Name = "Test User",
                Email = $"{emailPrefix}@gmail.com",
                Gender = "Male",
                Status = "active"
            };

            await apiClient.Users.CreateUserAsync(user);
            var users = await apiClient.Users.GetUsersAsync();
            var createdUser = users.Find(u => u.Email == user.Email);
            createdUser.Should().NotBeNull();

            int userId = createdUser.Id;

            var todoItem = new TodoItem
            {
                Title = $"Test Todo Item from {user.Name}",
                Status = "pending"
            };

            await apiClient.UserTodoItems.CreateUserTodoItemAsync(userId, todoItem);
            var userTodoItems = await apiClient.UserTodoItems.GetUserTodoItemsAsync(userId);
            userTodoItems.Should().NotBeNullOrEmpty();

            var createdTodoItem = userTodoItems.Find(t => t.Title == todoItem.Title);
            createdTodoItem.Should().NotBeNull();
            userId.Should().Be(userId);
            createdTodoItem.Status.Should().BeEquivalentTo(todoItem.Status);

            await apiClient.Users.DeleteUserAsync(userId);

            var updatedUserList = await apiClient.Users.GetUsersAsync();
            updatedUserList.Should().NotContain(user);

            userTodoItems = await apiClient.UserTodoItems.GetUserTodoItemsAsync(userId);
            userTodoItems.Count.Should().Be(0);
        }
    }
}