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
            var emmailPrefix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            var user = new User
            {
                Name = "Test User",
                Email = $"{emmailPrefix}@gmail.com",
                Gender = "Male",
                Status = "active"
            };

            await apiClient.CreateUserAsync(user);

            var users = await apiClient.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var createdUser = users.Find(u => u.Email == user.Email);

            createdUser.Should().NotBeNull();
            createdUser.Name.Should().BeEquivalentTo(user.Name);
            createdUser.Gender.Should().BeEquivalentTo(user.Gender);
            createdUser.Status.Should().BeEquivalentTo(user.Status);

            createdUser.Status = "inactive";

            await apiClient.UpdateUserAsync(createdUser.Id, createdUser);

            users = await apiClient.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var updatedUser = users.Find(u => u.Email == createdUser.Email);
            updatedUser.Should().NotBeNull();
            updatedUser.Status.Should().BeEquivalentTo(createdUser.Status);

            string newEmail = $"{emmailPrefix}updated@gmail.com";

            object partialUserUpdateData = new
            {
                Email = newEmail
            };

            await apiClient.UpdateUserPartiallyAsync(updatedUser.Id, partialUserUpdateData);

            users = await apiClient.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var partiallyUpdatedUser = users.Find(u => u.Email == newEmail);
            partiallyUpdatedUser.Should().NotBeNull();
            partiallyUpdatedUser.Email.Should().Be(newEmail);

            await apiClient.DeleteUserAsync(partiallyUpdatedUser.Id);

            var updatedUserList = await apiClient.GetUsersAsync();
            updatedUserList.Should().NotContain(partiallyUpdatedUser);
        }
    }
}