using SeleniumWebDriver.Business.API;
using SeleniumWebDriverTask.Core.Utilities;
using RestSharp;
using SeleniumWebDriver.Business.Models;
using System.Net;

namespace SeleniumWebDriverTask.Tests.TestsAPI
{
    public class ApiTests
    {
        private readonly ApiClient _apiClient;

        public ApiTests()
        {
            _apiClient = new ApiClient();
        }

        [Fact]
        public async Task ValidateGetUsers_ReturnsListOfUsers()
        {
            LoggerHelper.LogInformation("Starting test for ValidateGetUsers_ReturnsListOfUsers");
            var apiClient = new ApiClient();
            var response = await _apiClient.GetUsersAsync();

            ValidateResponse(response);

            Assert.NotNull(response.Data);
            Assert.True(response.Data.Count > 0);

            Assert.All(response.Data, user =>
            {
                Assert.False(string.IsNullOrEmpty(user.Id.ToString()), "ID is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Name), "Name is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Username), "Username is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Email), "Email is null or empty");

                Assert.NotNull(user.Address);
                Assert.False(string.IsNullOrEmpty(user.Address.Street), "Street is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Address.City), "City is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Address.Zipcode), "Zipcode is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Address.Suite), "Suite is null or empty");

                Assert.NotNull(user.Address.Geo);
                Assert.False(string.IsNullOrEmpty(user.Address.Geo.Lat), "Latitude is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Address.Geo.Lng), "Longitude is null or empty");

                Assert.False(string.IsNullOrEmpty(user.Phone), "Phone is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Website), "Website is null or empty");

                Assert.NotNull(user.Company);
                Assert.False(string.IsNullOrEmpty(user.Company.Name), "Company Name is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Company.CatchPhrase), "CatchPhrase is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Company.Bs), "BS is null or empty");
            });

            LoggerHelper.LogInformation($"Users list successfully validated and received a 200 OK response.");
        }

        [Fact]
        public async Task ValidateGetUsers_ContentTypeHeader()
        {
            LoggerHelper.LogInformation("Starting test for ValidateGetUsers_ContentTypeHeader");

            var apiClient = new ApiClient();
            var response = await _apiClient.GetUsersAsync();

            ValidateResponse(response);

            Assert.NotNull(response.ContentType);
            Assert.Contains(response.ContentType, "application/json; charset=utf-8");

            LoggerHelper.LogInformation($"Content-Type is correct and received a 200 OK response.");
        }

        [Fact]
        public async Task ValidateGetUsers_ResponseListOfUsers()
        {
            LoggerHelper.LogInformation("Starting test for ValidateGetUsers_ResponseListOfUsers");

            var apiClient = new ApiClient();
            var response = await _apiClient.GetUsersAsync();

            ValidateResponse(response);

            var users = response.Data;
            Assert.NotNull(users);
            Assert.Equal(10, users.Count);

            var distinctUserIds = users.Select(user => user.Id).Distinct().Count();
            Assert.Equal(10, distinctUserIds);

            Assert.All(users, user =>
            {
                Assert.False(string.IsNullOrEmpty(user.Name), "User Name is null or empty");
                Assert.False(string.IsNullOrEmpty(user.Username), "User Username is null or empty");
            });

            Assert.All(users, user =>
            {
                Assert.NotNull(user.Company);
                Assert.False(string.IsNullOrEmpty(user.Company.Name), "Company Name is null or empty");
            });

            Assert.True(response.IsSuccessful, "API request was not successful.");

            LoggerHelper.LogInformation($"Users list successfully validated and received a 200 OK response.");
        }

        [Fact]
        public async Task ValidatePostUsers_CanBeCreated()
        {
            LoggerHelper.LogInformation("Starting test for  ValidatePostUsers_CanBeCreated");

            var apiClient = new ApiClient();
            var newUser = new UserModel { Name = "Doe", Username = "John" };
            var response = await _apiClient.CreateUsersAsync(newUser);

            var createdUser = await apiClient.CreateUsersAsync(newUser);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.NotEmpty(response.Data.Id.ToString());
            Assert.Equal(newUser.Username, response.Data.Username);

            LoggerHelper.LogInformation($"User successfully created with ID, name , username and received a 200 OK response.");
        }

        [Fact]
        public async Task ValidateGetUsers_IfResorceDoseNotExist()
        {
            LoggerHelper.LogInformation("Starting test for ValidateGetUsers_IfResorceDoseNotExist");

            var apiClient = new ApiClient();
            var response = await _apiClient.GetInvalidEndpointAsync("/invalidendpoint");

            Assert.Null(response.ErrorMessage);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            LoggerHelper.LogInformation($"Users list validated when resorce doesn't existand received a 200 OK response.");
        }

        private static void ValidateResponse<T>(RestResponse<T> response)
        {
            Assert.NotNull(response);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Null(response.ErrorMessage);
        }
    }
}