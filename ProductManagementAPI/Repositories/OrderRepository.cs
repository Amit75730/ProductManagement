using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Repositories.Interfaces;

namespace ProductManagementAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)  // Ensure Product is loaded
                .ToList();
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)  // Ensure Product is loaded
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
