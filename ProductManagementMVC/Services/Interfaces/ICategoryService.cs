using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(int id);
    }
}
