using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category? GetCategoryById(int id);
    }
}
