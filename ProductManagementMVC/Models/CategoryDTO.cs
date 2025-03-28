using System.ComponentModel.DataAnnotations;

namespace ProductManagementMVC.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty; // Default value
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

}
