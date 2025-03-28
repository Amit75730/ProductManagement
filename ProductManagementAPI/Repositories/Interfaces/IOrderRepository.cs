using ProductManagementAPI.Models;

namespace ProductManagementAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetUserOrders(int userId);
        Order? GetOrderById(int orderId);
        void CreateOrder(Order order);
        bool SaveChanges();
        //Order? GetOrderByProductId(int productId);  // Add this method
    }
}
