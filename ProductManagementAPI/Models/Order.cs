using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        // public int CartId { get; set; }
        // public Cart? Cart { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Completed, Failed
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
