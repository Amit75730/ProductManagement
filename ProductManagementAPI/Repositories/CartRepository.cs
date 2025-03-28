using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories.Interfaces;

namespace ProductManagementAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all cart items without user dependency
        public IEnumerable<Cart> GetCartItems()
        {
            return _context.Carts
                .Include(c => c.Product)
                .ToList();
        }

        // Get a specific cart item
        public Cart? GetCartItem(int productId)
        {
            return _context.Carts.FirstOrDefault(c => c.ProductId == productId);
        }

        public void AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public void RemoveFromCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
