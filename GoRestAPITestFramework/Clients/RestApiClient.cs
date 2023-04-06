using GoRestAPITestFramework.Repositories.Interfaces;
using GoRestAPITestFramework.Repositories;
using RestSharp;

public class RestApiClient
{
    private readonly RestClient _client;

    public IUserRepository Users { get; }
    public IUserPostsRepository UserPosts { get; }
    public IPostCommentsRepository PostComments { get; }
    public IUserTodoItemRepository UserTodoItems { get; }

    private static readonly string BaseUrl = "https://gorest.co.in/public/v2";
    private static readonly string AccessTokenFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clients", "GoRestApiToken.txt");
    private static readonly string AccessToken = ReadAccessTokenFromFile(AccessTokenFilePath);

    public RestApiClient()
    {
        _client = new RestClient(BaseUrl);
        _client.AddDefaultHeader("Authorization", $"Bearer {AccessToken}");

        Users = new UserRepository(_client);
        UserPosts = new UserPostRepository(_client);
        PostComments = new PostCommentRepository(_client);
        UserTodoItems = new UserTodoItemRepository(_client);
    }

    private static string ReadAccessTokenFromFile(string filePath)
    {
        return File.ReadAllText(filePath).Trim();
    }
}