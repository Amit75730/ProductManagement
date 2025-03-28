namespace ProductManagementMVC.Models
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // You can rename this to PaymentStatus if needed
        public List<OrderItemDTO> OrderItems { get; set; } // Include OrderItems here
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
