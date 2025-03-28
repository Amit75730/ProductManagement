using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;

namespace ProductManagementMVC.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CartService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<CartItemDTO>> GetCartItems()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/cart");
            if (!response.IsSuccessStatusCode) return null;
            return JsonSerializer.Deserialize<List<CartItemDTO>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> AddToCart(int productId, int quantity)
        {
            var cartAction = new { ProductId = productId, Quantity = quantity };
            var content = new StringContent(JsonSerializer.Serialize(cartAction), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/cart/add", content);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RemoveFromCart(int productId)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/cart/remove/{productId}");
            return response.IsSuccessStatusCode;
        }
    }
}
