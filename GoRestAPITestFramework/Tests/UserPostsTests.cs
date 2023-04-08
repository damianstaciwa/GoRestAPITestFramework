using FluentAssertions;
using GoRestAPITestFramework.Models;
using NUnit.Framework;

namespace GoRestAPITestFramework
{
    [TestFixture]
    public class UserPostsTests
    {
        private readonly RestApiClient apiClient;

        public UserPostsTests()
        {
            apiClient = new RestApiClient();
        }

        [Test]
        public async Task FullUserPostsCRUDTest()
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

            var post = new Post
            {
                Title = $"Test Post from {user.Name}",
                Body = "This is a test post for user posts test."
            };

            await apiClient.UserPosts.CreateUserPostAsync(userId, post);

            var userPosts = await apiClient.UserPosts.GetUserPostsAsync(userId);
            userPosts.Should().NotBeNullOrEmpty();

            var createdPost = userPosts.Find(p => p.Title == post.Title);
            createdPost.Should().NotBeNull();
            createdPost.User_Id.Should().Be(userId);
            createdPost.Body.Should().BeEquivalentTo(post.Body);
        }
    }
}