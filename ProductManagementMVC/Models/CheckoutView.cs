namespace ProductManagementMVC.Models
{
    public class CheckoutView
    {
        public decimal TotalAmount { get; set; }
        public List<OrderItemView> OrderItems { get; set; } = new();  // 🔹 Renamed from "CartItems"
    }
}
