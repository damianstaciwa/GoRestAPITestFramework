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
        public async Task FullPostCommentsCRUDTest()
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
                Body = "This is a test post for post comments test."
            };

            await apiClient.UserPosts.CreateUserPostAsync(userId, post);
            var userPosts = await apiClient.UserPosts.GetUserPostsAsync(userId);
            var createdPost = userPosts.Find(p => p.Title == post.Title);
            createdPost.Should().NotBeNull();

            int postId = createdPost.Id;

            var comment = new Comment
            {
                Name = "Test Comment",
                Email = $"test_comment_{emailPrefix}@gmail.com",
                Body = "This is a test comment for post comments test."
            };

            await apiClient.PostComments.CreateCommentForPostAsync(postId, comment);

            var postComments = await apiClient.PostComments.GetPostCommentsAsync(postId);
            postComments.Should().NotBeNullOrEmpty();

            var createdComment = postComments.Find(c => c.Email == comment.Email);
            createdComment.Should().NotBeNull();
            createdComment.Name.Should().BeEquivalentTo(comment.Name);
            createdComment.Body.Should().BeEquivalentTo(comment.Body);

            await apiClient.Users.DeleteUserAsync(userId);

            var updatedUserList = await apiClient.Users.GetUsersAsync();
            updatedUserList.Should().NotContain(user);

            postComments = await apiClient.PostComments.GetPostCommentsAsync(postId);
            postComments.Should().NotContain(createdComment);
        }
    }
}