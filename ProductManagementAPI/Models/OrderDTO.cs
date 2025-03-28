namespace ProductManagementAPI.Models
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string? PaymentStatus { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>(); // Added OrderItems
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Product name
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
