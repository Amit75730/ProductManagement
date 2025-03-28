using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        Product? GetProductById(int id);
       // void LockProduct(int productId);
        //void UnlockProduct(int productId);
        IEnumerable<Product> GetAllProducts();
    }
}
