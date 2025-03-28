using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories == null)
            {
                categories = new List<CategoryDTO>(); // Empty list instead of null
            }
            return View(categories);
        }

        public async Task<IActionResult> Products(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            return View("Products", category); // Passing category data to the view
        }
    }
}
