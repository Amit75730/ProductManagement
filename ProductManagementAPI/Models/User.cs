using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
