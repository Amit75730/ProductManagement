using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        Product? GetProductById(int id);
        IEnumerable<Product> GetAllProducts();
        //void LockProduct(int productId);
        //void UnlockProduct(int productId);
        //void UnlockExpiredLockedProducts();
    }
}
