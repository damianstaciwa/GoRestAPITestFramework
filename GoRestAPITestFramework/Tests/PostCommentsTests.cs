using FluentAssertions;
using GoRestAPITestFramework.Models;
using NUnit.Framework;

namespace GoRestAPITestFramework
{
    [TestFixture]
    public class PostCommentsTests
    {
        private readonly RestApiClient apiClient;

        public PostCommentsTests()
        {
            apiClient = new RestApiClient();
        }

        [Test]
        public async Task GetPostCommentsTest()
        {
            int postId = await GetTestPostIdAsync();

            var postComments = await apiClient.PostComments.GetPostCommentsAsync(postId);
            postComments.Should().NotBeNull();
        }

        [Test]
        public async Task CreateCommentForPostTest()
        {
            var emailSuffix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            int postId = await GetTestPostIdAsync();

            var comment = new Comment
            {
                Name = "Test Comment",
                Email = $"test_comment_{emailSuffix}@gmail.com",
                Body = "This is a test comment for post comments test."
            };

            await apiClient.PostComments.CreateCommentForPostAsync(postId, comment);

            var postComments = await apiClient.PostComments.GetPostCommentsAsync(postId);
            postComments.Should().NotBeNullOrEmpty();

            var createdComment = postComments.Find(c => c.Email == comment.Email);
            createdComment.Should().NotBeNull();
            createdComment.Name.Should().BeEquivalentTo(comment.Name);
            createdComment.Body.Should().BeEquivalentTo(comment.Body);
        }

        private async Task<int> GetTestPostIdAsync()
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

            var post = new Post
            {
                Title = $"Test Post from {user.Name}",
                Body = "This is a test post for post comments test."
            };

            await apiClient.UserPosts.CreateUserPostAsync(userId, post);
            var userPosts = await apiClient.UserPosts.GetUserPostsAsync(userId);
            var createdPost = userPosts.Find(p => p.Title == post.Title);

            int postId = createdPost.Id;

            return postId;
        }
    }
}