using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Interfaces;
using System.Security.Claims;

namespace ProductManagementAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get all cart items
        [HttpGet]
        public IActionResult GetCartItems()
        {
            return Ok(_cartService.GetCartItems());
        }

        // Add product to cart
        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] CartActionDTO cartAction)
        {
            var result = _cartService.AddToCart(cartAction.ProductId, cartAction.Quantity);
            return result ? Ok("Product added to cart.") : BadRequest("Unable to add product.");
        }

        // Remove product from cart
        [HttpDelete("remove/{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var result = _cartService.RemoveFromCart(productId);
            return result ? Ok("Product removed from cart.") : NotFound("Product not found in cart.");
        }
    }
}
