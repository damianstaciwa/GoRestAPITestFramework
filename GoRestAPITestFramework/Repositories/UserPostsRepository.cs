﻿using GoRestAPITestFramework.Models;
using GoRestAPITestFramework.Repositories.Interfaces;
using RestSharp;

namespace GoRestAPITestFramework.Repositories
{
    public class UserPostRepository : IUserPostsRepository
    {
        private readonly RestClient _client;

        public UserPostRepository(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Post>> GetUserPostsAsync(int userId)
        {
            var request = new RestRequest($"users/{userId}/posts", Method.Get);
            var response = await _client.ExecuteAsync<List<Post>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<Post>();
            }
        }

        public async Task CreateUserPostAsync(int userId, Post newPost)
        {
            var request = new RestRequest($"users/{userId}/posts", Method.Post);
            request.AddJsonBody(newPost);
            var response = await _client.ExecuteAsync(request);

            ApiUtils.HandleResponse(response);
        }
    }
}