using RestSharp;

namespace GoRestAPITestFramework
{
    public static class ApiUtils
    {
        public static void HandleResponse(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                throw new InvalidOperationException($"Request failed: {response.StatusCode} - {response.StatusDescription}");
            }
        }
    }
}