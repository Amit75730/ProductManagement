using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Repositories.Interfaces;
using ProductManagementAPI.Services.Interfaces;

namespace ProductManagementAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public IEnumerable<OrderDTO> GetUserOrders(int userId)
        {
            var orders = _orderRepository.GetUserOrders(userId);  // Get orders from the repository
            return orders.Select(order => new OrderDTO
            {
                OrderId = order.OrderId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product != null ? oi.Product.Name : "Product not found",
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() // Include products in the order
            }).ToList();
        }

        public OrderDTO? GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null) return null;

            return new OrderDTO
            {
                OrderId = order.OrderId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product != null ? oi.Product.Name : "Product not found",
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList() // Include products in the order
            };
        }

        public bool PlaceOrder(int userId)
        {
            var cartItems = _cartRepository.GetCartItems().ToList();
            if (!cartItems.Any()) return false; // Cart is empty

            decimal totalAmount = cartItems.Sum(c => (c.Product?.Price ?? 0) * c.Quantity);

            var order = new Order
            {
                UserId = userId,
                TotalAmount = totalAmount,
                PaymentStatus = "Completed", // Simulate successful payment
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList() // Add products to the order items
            };

            _orderRepository.CreateOrder(order);

            // Optionally clear the cart after the order is placed
            foreach (var item in cartItems)
            {
                _cartRepository.RemoveFromCart(item); // Remove from cart after placing order
            }

            return _orderRepository.SaveChanges();
        }
    }
}
