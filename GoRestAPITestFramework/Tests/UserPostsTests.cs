﻿using FluentAssertions;
using FluentAssertions.Execution;
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
        public async Task GetUserPostsTest()
        {
            // arrange
            int userId = await GetTestUserIdAsync();

            // act
            var userPosts = await apiClient.UserPosts.GetUserPostsAsync(userId);

            // assert
            userPosts.Should().NotBeNull();
        }

        [Test]
        public async Task CreateUserPostTest()
        {
            // arrange
            int userId = await GetTestUserIdAsync();

            var post = new Post
            {
                Title = $"Test Post",
                Body = "This is a test post for user posts test."
            };

            // act
            await apiClient.UserPosts.CreateUserPostAsync(userId, post);

            var userPosts = await apiClient.UserPosts.GetUserPostsAsync(userId);
            userPosts.Should().NotBeNullOrEmpty();

            var createdPost = userPosts.Find(p => p.Title == post.Title);

            // assert
            using (new AssertionScope())
            {
                createdPost.Should().NotBeNull();
                createdPost.User_Id.Should().Be(userId);
                createdPost.Body.Should().BeEquivalentTo(post.Body);
            }
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
            return createdUser.Id;
        }
    }
}
