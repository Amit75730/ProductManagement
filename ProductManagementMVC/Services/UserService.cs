using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ProductManagementMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> RegisterUser(UserRegisterDTO userDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/user/register", content);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }

        public async Task<string> LoginUser(UserLoginDTO userDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/user/login", content);
            if (response.IsSuccessStatusCode)
            {
                // Store the JWT Token in session
                var token = await response.Content.ReadAsStringAsync();
                _httpContextAccessor.HttpContext.Session.SetString("JWTToken", token);
                Console.WriteLine("Token: " + token);
                return token;
            }
            return null;
        }

        // Retrieve JWT Token from session
        public string GetToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
        }
    }
}
