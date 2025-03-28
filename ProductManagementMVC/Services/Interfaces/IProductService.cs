using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<ProductDTO> GetProductById(int id);
    }
}
