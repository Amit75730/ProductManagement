using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductManagementMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            await _cartService.AddToCart(productId, quantity);
            return RedirectToAction("Index", "Cart"); // Redirect to cart after adding
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }
    }
}
