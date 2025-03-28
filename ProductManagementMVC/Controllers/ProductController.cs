using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductManagementMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}
