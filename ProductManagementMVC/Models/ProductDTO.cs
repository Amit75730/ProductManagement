namespace ProductManagementMVC.Models
{
public class ProductDTO
{
    public int ProductId  { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } // Optional for category name display in product list
}

}
