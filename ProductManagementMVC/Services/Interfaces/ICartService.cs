using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemDTO>> GetCartItems();
        Task<bool> AddToCart(int productId, int quantity);
        Task<bool> RemoveFromCart(int productId);
    }
}
