using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Repositories.Interfaces;

namespace ProductManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList(); // Retrieve all products from the database
        }

        // Lock the product
        public void LockProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.IsLocked = true;
                product.LockTime = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }

        // Unlock the product
        public void UnlockProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.IsLocked = false;
                product.LockTime = null;
                _context.SaveChanges();
            }
        }
    }
}
