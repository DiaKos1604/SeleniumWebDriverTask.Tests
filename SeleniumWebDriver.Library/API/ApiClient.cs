﻿using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using SeleniumWebDriverTask.Core.Utilities;
using SeleniumWebDriver.Business.Models;

namespace SeleniumWebDriver.Business.API
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

        private RestRequest CreateRequest(string resource, Method method = Method.Get)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Accept", "application/json");
            LoggerHelper.LogInformation($"Creating request for: {resource}");
            return request;
        }

        public async Task<RestResponse<List<UserModel>>> GetUsersAsync()
        {
            var request = CreateRequest("/users");
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
            var request = CreateRequest("/users", Method.Post);
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
            var request = CreateRequest(resource, Method.Get);
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