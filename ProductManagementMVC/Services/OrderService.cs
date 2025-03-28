using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ProductManagementMVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
            _httpContextAccessor = httpContextAccessor;
        }

        // Fetch all user orders
public async Task<List<OrderDTO>> GetUserOrders()
{
    var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
    if (string.IsNullOrEmpty(token))
    {
        return null; // No token means user is not authenticated.
    }

    var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiBaseUrl}/api/orders");
    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    
    var response = await _httpClient.SendAsync(request);
    if (!response.IsSuccessStatusCode) return null;

    return JsonSerializer.Deserialize<List<OrderDTO>>(await response.Content.ReadAsStringAsync());
}


        // Fetch a specific order by ID
        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiBaseUrl}/api/orders/{orderId}");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("Authorization Header: " + request.Headers.Authorization);

            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            return JsonSerializer.Deserialize<OrderDTO>(await response.Content.ReadAsStringAsync());
        }

        // Place a new order
        public async Task<bool> PlaceOrder()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return false;  // Token is missing, likely redirecting due to lack of authentication.
            }

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/api/orders/checkout");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

    }
}
