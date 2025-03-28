using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Repositories.Interfaces;
using ProductManagementAPI.Services.Interfaces;

namespace ProductManagementAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        // Get all cart items (no user dependency)
        public IEnumerable<CartDTO> GetCartItems()
        {
            return _cartRepository.GetCartItems().Select(cart => new CartDTO
            {
                ProductId = cart.ProductId,
                ProductName = cart.Product?.Name ?? "",
                Price = cart.Product?.Price ?? 0,
                Quantity = cart.Quantity,
                TotalPrice = (cart.Product?.Price ?? 0) * cart.Quantity
            }).ToList();
        }

        // Add a product to the cart
        public bool AddToCart(int productId, int quantity)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null || product.Stock < quantity)
            {
                return false; // Product not found or insufficient stock
            }

            var existingCartItem = _cartRepository.GetCartItem(productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                _cartRepository.AddToCart(new Cart
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            product.Stock -= quantity; // Reduce stock after adding to cart
            return _cartRepository.SaveChanges();
        }

        // Remove product from cart
        public bool RemoveFromCart(int productId)
        {
            var cartItem = _cartRepository.GetCartItem(productId);
            if (cartItem == null) return false;

            _cartRepository.RemoveFromCart(cartItem);
            return _cartRepository.SaveChanges();
        }
    }
}
