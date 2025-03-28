using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagementMVC.Models;

namespace ProductManagementMVC.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetUserOrders();
        Task<OrderDTO> GetOrderById(int orderId);
        Task<bool> PlaceOrder();
    }
}
