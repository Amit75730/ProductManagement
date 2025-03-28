using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductManagementMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // View all orders in the order history
        public async Task<IActionResult> OrderHistory()
        {
            var orders = await _orderService.GetUserOrders();
            if (orders == null || orders.Count == 0)
            {
                ViewBag.Message = "You have no orders yet.";
                return View(new List<OrderDTO>());
            }
            return View(orders);
        }

        // View details of a specific order
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("OrderHistory");
            }
            return View(order);
        }

        // Place a new order and redirect to OrderHistory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                // Redirect to login if the user is not authenticated
                return RedirectToAction("Login", "User");
            }
            var result = await _orderService.PlaceOrder();

            if (result)
            {
                TempData["SuccessMessage"] = "Your order has been placed successfully.";
                return RedirectToAction("OrderHistory");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to place the order. Please try again.";
                return RedirectToAction("OrderHistory");
            }
        }
    }
}
