using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories.Interfaces;

namespace ProductManagementAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
