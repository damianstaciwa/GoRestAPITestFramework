using FluentAssertions;
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
        public async Task FullUserCRUDTest()
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

            createdUser.Should().NotBeNull();
            createdUser.Name.Should().BeEquivalentTo(user.Name);
            createdUser.Gender.Should().BeEquivalentTo(user.Gender);
            createdUser.Status.Should().BeEquivalentTo(user.Status);

            createdUser.Status = "inactive";

            await apiClient.Users.UpdateUserAsync(createdUser.Id, createdUser);

            users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var updatedUser = users.Find(u => u.Email == createdUser.Email);
            updatedUser.Should().NotBeNull();
            updatedUser.Status.Should().BeEquivalentTo(createdUser.Status);

            string newEmail = $"{emailPrefix}updated@gmail.com";

            object partialUserUpdateData = new
            {
                Email = newEmail
            };

            await apiClient.Users.UpdateUserPartiallyAsync(updatedUser.Id, partialUserUpdateData);

            users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var partiallyUpdatedUser = users.Find(u => u.Email == newEmail);
            partiallyUpdatedUser.Should().NotBeNull();
            partiallyUpdatedUser.Email.Should().Be(newEmail);

            await apiClient.Users.DeleteUserAsync(partiallyUpdatedUser.Id);

            var updatedUserList = await apiClient.Users.GetUsersAsync();
            updatedUserList.Should().NotContain(partiallyUpdatedUser);
        }
    }
}