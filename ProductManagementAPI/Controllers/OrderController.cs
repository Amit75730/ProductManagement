using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;
using System.Security.Claims;

namespace ProductManagementAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetUserOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return Ok(_orderService.GetUserOrders(userId));
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            return order != null ? Ok(order) : NotFound("Order not found.");
        }

        [HttpPost("checkout")]
        public IActionResult PlaceOrder()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = _orderService.PlaceOrder(userId);
            return result ? Ok("Order placed successfully.") : BadRequest("Failed to place order.");
        }
    }
}
