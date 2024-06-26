﻿using FluentAssertions;
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
            // arrange
            var emailPrefix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            var user = new User
            {
                Name = "Test User",
                Email = $"{emailPrefix}@gmail.com",
                Gender = "Male",
                Status = "active"
            };

            // act
            await apiClient.Users.CreateUserAsync(user);

            var users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var createdUser = users.Find(u => u.Email == user.Email);

            // assert
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
            // arrange
            var user = await GetTestUserAsync();

            user.Status = "inactive";
            await apiClient.Users.UpdateUserAsync(user.Id, user);

            // act
            var users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var updatedUser = users.Find(u => u.Email == user.Email);

            // assert
            using (new AssertionScope())
            {
                updatedUser.Should().NotBeNull();
                updatedUser.Status.Should().BeEquivalentTo(user.Status);
            }
        }

        [Test]
        public async Task UpdateUserPartiallyTest()
        {
            // arrange
            var newEmailPrefix = DateTime.Now.ToString("ddMMyyyyHHmmss");

            var user = await GetTestUserAsync();

            string newEmail = $"{newEmailPrefix}updated@gmail.com";

            object partialUserUpdateData = new { Email = newEmail };

            // act
            await apiClient.Users.UpdateUserPartiallyAsync(user.Id, partialUserUpdateData);

            var users = await apiClient.Users.GetUsersAsync();
            users.Should().NotBeNullOrEmpty();

            var partiallyUpdatedUser = users.Find(u => u.Email == newEmail);

            // assert
            using (new AssertionScope())
            {
                partiallyUpdatedUser.Should().NotBeNull();
                partiallyUpdatedUser.Email.Should().Be(newEmail);
            }
        }

        [Test]
        public async Task DeleteUserTest()
        {
            // arrange
            var user = await GetTestUserAsync();

            // act
            await apiClient.Users.DeleteUserAsync(user.Id);

            var updatedUserList = await apiClient.Users.GetUsersAsync();

            // assert
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
