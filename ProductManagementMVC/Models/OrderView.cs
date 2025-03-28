namespace ProductManagementMVC.Models
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public List<OrderItemView> OrderItems { get; set; } = new();
    }

    public class OrderItemView
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
