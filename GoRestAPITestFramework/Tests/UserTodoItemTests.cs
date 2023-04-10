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
        public async Task GetUserTodoItemsTest()
        {
            int userId = await GetTestUserIdAsync();

            var userTodoItems = await apiClient.UserTodoItems.GetUserTodoItemsAsync(userId);
            userTodoItems.Should().NotBeNull();
        }

        [Test]
        public async Task CreateUserTodoItemTest()
        {
            int userId = await GetTestUserIdAsync();

            var todoItem = new TodoItem
            {
                Title = $"Test Todo Item from {userId}",
                Status = "pending"
            };

            await apiClient.UserTodoItems.CreateUserTodoItemAsync(userId, todoItem);
            var userTodoItems = await apiClient.UserTodoItems.GetUserTodoItemsAsync(userId);
            userTodoItems.Should().NotBeNullOrEmpty();

            var createdTodoItem = userTodoItems.Find(t => t.Title == todoItem.Title);
            createdTodoItem.Should().NotBeNull();
            createdTodoItem.User_Id.Should().Be(userId);
            createdTodoItem.Status.Should().BeEquivalentTo(todoItem.Status);
        }
        private async Task<int> GetTestUserIdAsync()
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

            int userId = createdUser.Id;

            return userId;
        }
    }
}