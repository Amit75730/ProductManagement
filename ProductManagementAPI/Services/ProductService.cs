using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Repositories.Interfaces;
using ProductManagementAPI.Services.Interfaces;

namespace ProductManagementAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategory(categoryId);
        }

        public Product? GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts(); // Retrieve all products from the database
        }
        // Lock a product
        // public void LockProduct(int productId)
        // {
        //     _productRepository.LockProduct(productId);
        // }

        // // Unlock a product
        // public void UnlockProduct(int productId)
        // {
        //     _productRepository.UnlockProduct(productId);
        // }

        // Check if a product should be unlocked after 2 minutes
        // public void UnlockExpiredLockedProducts()
        // {
        //     var products = _productRepository.GetAllProducts();
        //     foreach (var product in products)
        //     {
        //         if (product.IsLocked && product.LockTime.HasValue)
        //         {
        //             if (product.LockTime.Value.AddMinutes(2) < DateTime.UtcNow)
        //             {
        //                 UnlockProduct(product.ProductId);
        //             }
        //         }
        //     }
        // }

    }
}
