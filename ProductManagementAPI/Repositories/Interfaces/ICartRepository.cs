using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetCartItems();
        Cart? GetCartItem(int productId);
        void AddToCart(Cart cart);
        void RemoveFromCart(Cart cart);
        bool SaveChanges();
    }
}
