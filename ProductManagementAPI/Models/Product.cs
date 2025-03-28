using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty; // e.g., kg, litre, pcs

        [Required]
        public int Stock { get; set; } = 200; // Default stock

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsLocked { get; set; } = false;
        public DateTime? LockTime { get; set; } // Store lock time
        public Product? product{get; set;}
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
