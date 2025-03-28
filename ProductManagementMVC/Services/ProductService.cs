using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;

namespace ProductManagementMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/products/category/{categoryId}");
            if (!response.IsSuccessStatusCode) return new List<ProductDTO>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProductDTO>();
        }


        public async Task<ProductDTO> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/products/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return JsonSerializer.Deserialize<ProductDTO>(await response.Content.ReadAsStringAsync());
        }
    }
}
