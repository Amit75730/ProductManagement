using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services.Interfaces
{
    public interface ICartService
    {
        IEnumerable<CartDTO> GetCartItems();
        bool AddToCart(int productId, int quantity);
        bool RemoveFromCart(int productId);
    }
}
