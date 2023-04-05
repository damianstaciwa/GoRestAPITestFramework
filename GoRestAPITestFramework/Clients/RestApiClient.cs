using GoRestAPITestFramework.Models;
using RestSharp;

public class RestApiClient
{
    private readonly RestClient _client;

    private static readonly string BaseUrl = "https://gorest.co.in/public/v2";
    private static readonly string AccessTokenFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clients", "GoRestApiToken.txt");
    private static readonly string AccessToken = ReadAccessTokenFromFile(AccessTokenFilePath);

    public RestApiClient()
    {
        _client = new RestClient(BaseUrl);
        _client.AddDefaultHeader("Authorization", $"Bearer {AccessToken}");
    }

    private static string ReadAccessTokenFromFile(string filePath)
    {
        return File.ReadAllText(filePath).Trim();
    }

    #region Users

    public async Task<List<User>> GetUsersAsync()
    {
        var request = new RestRequest("users", Method.Get);
        var response = await _client.ExecuteAsync<List<User>>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data;
        }
        else
        {
            return new List<User>();
        }
    }

    public async Task CreateUserAsync(User user)
    {
        var request = new RestRequest("users", Method.Post);
        request.AddJsonBody(user);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateUserAsync(int userId, User user)
    {
        var request = new RestRequest($"users/{userId}", Method.Put);
        request.AddJsonBody(user);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateUserPartiallyAsync(int userId, object partialUpdate)
    {
        var request = new RestRequest($"users/{userId}", Method.Patch);
        request.AddJsonBody(partialUpdate);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var request = new RestRequest($"users/{userId}", Method.Delete);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    #region Posts
    public async Task<List<Post>> GetPostsAsync()
    {
        var request = new RestRequest("posts", Method.Get);
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

    public async Task CreatePostAsync(Post post)
    {
        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(post);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdatePostAsync(int postId, Post post)
    {
        var request = new RestRequest($"posts/{postId}", Method.Put);
        request.AddJsonBody(post);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdatePostPartiallyAsync(int postId, object partialUpdate)
    {
        var request = new RestRequest($"posts/{postId}", Method.Patch);
        request.AddJsonBody(partialUpdate);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task DeletePostAsync(int postId)
    {
        var request = new RestRequest($"posts/{postId}", Method.Delete);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion


    #region Comments
    public async Task<List<Comment>> GetCommentsAsync()
    {
        var request = new RestRequest("comments", Method.Get);
        var response = await _client.ExecuteAsync<List<Comment>>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data;
        }
        else
        {
            return new List<Comment>();
        }
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        var request = new RestRequest("comments", Method.Post);
        request.AddJsonBody(comment);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateCommentAsync(int commentId, Comment comment)
    {
        var request = new RestRequest($"comments/{commentId}", Method.Put);
        request.AddJsonBody(comment);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateCommentPartiallyAsync(int commentId, object partialUpdate)
    {
        var request = new RestRequest($"comments/{commentId}", Method.Patch);
        request.AddJsonBody(partialUpdate);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        var request = new RestRequest($"comments/{commentId}", Method.Delete);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    #region TodoItems
    public async Task<List<TodoItem>> GetTodosAsync()
    {
        var request = new RestRequest("todos", Method.Get);
        var response = await _client.ExecuteAsync<List<TodoItem>>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data;
        }
        else
        {
            return new List<TodoItem>();
        }
    }

    public async Task CreateTodoAsync(TodoItem todo)
    {
        var request = new RestRequest("todos", Method.Post);
        request.AddJsonBody(todo);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateTodoAsync(int todoId, TodoItem todo)
    {
        var request = new RestRequest($"todos/{todoId}", Method.Put);
        request.AddJsonBody(todo);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task UpdateTodoPartiallyAsync(int todoId, object partialUpdate)
    {
        var request = new RestRequest($"todos/{todoId}", Method.Patch);
        request.AddJsonBody(partialUpdate);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    public async Task DeleteTodoAsync(int todoId)
    {
        var request = new RestRequest($"todos/{todoId}", Method.Delete);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    #region UserPosts

    public async Task<List<Post>> GetUserPosts(int userId)
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

    public async Task CreateUserPost(int userId, Post newPost)
    {
        var request = new RestRequest($"users/{userId}/posts", Method.Post);
        request.AddJsonBody(newPost);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    #region PostComments

    public async Task<List<Comment>> GetPostComments(int postId)
    {
        var request = new RestRequest($"posts/{postId}/comments", Method.Get);
        var response = await _client.ExecuteAsync<List<Comment>>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data;
        }
        else
        {
            return new List<Comment>();
        }
    }

    public async Task CreatePostComment(int postId, Comment newComment)
    {
        var request = new RestRequest($"posts/{postId}/comments", Method.Post);
        request.AddJsonBody(newComment);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    #region UserTodoItems

    public async Task<List<TodoItem>> GetUserTodos(int userId)
    {
        var request = new RestRequest($"users/{userId}/todos", Method.Get);
        var response = await _client.ExecuteAsync<List<TodoItem>>(request);

        if (response.IsSuccessful && response.Data != null)
        {
            return response.Data;
        }
        else
        {
            return new List<TodoItem>();
        }
    }

    public async Task CreateUserTodoItem(int userId, TodoItem newTodo)
    {
        var request = new RestRequest($"users/{userId}/todos", Method.Post);
        request.AddJsonBody(newTodo);
        var response = await _client.ExecuteAsync(request);

        HandleResponse(response);
    }

    #endregion

    private void HandleResponse(RestResponse response)
    {
        if (!response.IsSuccessful)
        {
            throw new InvalidOperationException($"Request failed: {response.StatusCode} - {response.StatusDescription}");
        }
    }
}