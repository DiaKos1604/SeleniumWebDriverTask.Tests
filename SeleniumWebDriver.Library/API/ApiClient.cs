using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using SeleniumWebDriverTask.Core.Utilities;
using SeleniumWebDriverTask.Business.Models;

namespace SeleniumWebDriverTask.Business.API
{
    public class ApiClient
    {
        private readonly IRestClient _client;

        public ApiClient()
        {
            var baseUrl = ConfigurationHelper.GetApiBaseUrl();

            var serializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            _client = new RestClient(
                options: new(baseUrl),
                configureSerialization: s => s.UseSystemTextJson(serializerOptions));

            LoggerHelper.LogInformation($"API Client initialized with base URL: {baseUrl}");
        }

        public async Task<RestResponse<List<UserModel>>> GetUsersAsync()
        {
            var request = new RestRequest("/users", Method.Get);
            var response = await _client.ExecuteAsync<List<UserModel>>(request);

            if (response == null) 
            {
                LoggerHelper.LogError("Failed to get users.");
                throw new Exception("No response from server.");
            }

            return response;
        }

        public async Task<RestResponse<UserModel>> CreateUsersAsync(UserModel user)
        {
            var request = new RestRequest("/users", Method.Post);
            request.AddJsonBody(user);

            var response = await _client.ExecuteAsync<UserModel>(request);
            if (response == null)
            {
                LoggerHelper.LogError("Failed to create a new User.");
                throw new Exception("Failed to create a new User.");
            }

            LoggerHelper.LogInformation("Successfully created a User.");
            return response;
        }

        public async Task<RestResponse> GetInvalidEndpointAsync(string resource)
        {
            var request = new RestRequest(resource, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response == null)
            {
                LoggerHelper.LogError("No response from the server.");
                throw new Exception("Failed to get resource.");
            }

            LoggerHelper.LogInformation("Response Status Code: {StatusCode}");
            return response;
        }
    }
}