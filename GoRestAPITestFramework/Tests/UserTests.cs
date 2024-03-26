using FluentAssertions;
using FluentAssertions.Execution;
using GoRestAPITestFramework.Models;
using NUnit.Framework;

namespace GoRestAPITestFramework
{
    [TestFixture]
    public class UserTests
    {
        private readonly RestApiClient apiClient;

        public UserTests()
        {
            apiClient = new RestApiClient();
        }

        [Test]
        public async Task CreateUserTest()
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
            users.Should().NotBeNullOrEmpty();

            var createdUser = users.Find(u => u.Email == user.Email);

            using (new AssertionScope())
            {
                createdUser.Should().NotBeNull();
                createdUser.Name.Should().BeEquivalentTo(user.Name);
                createdUser.Gender.Should().BeEquivalentTo(user.Gender);
                createdUser.Status.Should().BeEquivalentTo(user.Status);
            }
        }

        [Test]
        public async Task UpdateUserTest()
        {
            var user = await GetTestUserAsync();

            user.Status = "inactive";
            await apiClient.Users.UpdateUserAsync(user.Id, user);

            var users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var updatedUser = users.Find(u => u.Email == user.Email);

            using (new AssertionScope())
            {
                updatedUser.Should().NotBeNull();
                updatedUser.Status.Should().BeEquivalentTo(user.Status);
            }
        }

        [Test]
        public async Task UpdateUserPartiallyTest()
        {
            var newEmailPrefix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            var user = await GetTestUserAsync();

            string newEmail = $"{newEmailPrefix}updated@gmail.com";

            object partialUserUpdateData = new { Email = newEmail };

            await apiClient.Users.UpdateUserPartiallyAsync(user.Id, partialUserUpdateData);

            var users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var partiallyUpdatedUser = users.Find(u => u.Email == newEmail);

            using (new AssertionScope())
            {
                partiallyUpdatedUser.Should().NotBeNull();
                partiallyUpdatedUser.Email.Should().Be(newEmail);
            }
        }

        [Test]
        public async Task DeleteUserTest()
        {
            var user = await GetTestUserAsync();

            await apiClient.Users.DeleteUserAsync(user.Id);

            var updatedUserList = await apiClient.Users.GetUsersAsync();
            updatedUserList.Should().NotContain(user);
        }

        private async Task<User> GetTestUserAsync()
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

            return users.Find(u => u.Email == user.Email);
        }
    }
}
