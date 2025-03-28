using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;

namespace ProductManagementMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CategoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/categories");

            if (!response.IsSuccessStatusCode)
            {
                return new List<CategoryDTO>(); // Return empty list on failure
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CategoryDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryDTO>();
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/categories/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
