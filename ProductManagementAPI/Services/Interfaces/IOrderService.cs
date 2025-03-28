using ProductManagementAPI.Models;

namespace ProductManagementAPI.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(int userId);
        OrderDTO? GetOrderById(int orderId);
        bool PlaceOrder(int userId);
    }
}
