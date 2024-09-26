using CardShop.Context;
using CardShop.Filters;
using CardShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CardShopDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CardShopDbContext context, ILogger<CategoryController> logger) {
            _context = context;
            _logger = logger;
        }

        [HttpGet("CategoryProducts")]
        public  async Task<ActionResult<IEnumerable<Category>>> GetProductsInCategoryAsync()
        {
            var category = await _context.categories.Include(p=> p.Products).ToListAsync();
            if (category is null)
            {
                _logger.LogWarning($"Category doesn't exist");
                return NotFound("Category not found");
            }

            return category;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryAsync()
        {
            var category = await _context.categories.ToListAsync();
            if (category is null)
            {
                _logger.LogWarning("Category doesn't exist");
                return NotFound("Category not found");
            }

            return category;
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id) 
        {
            var category = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category is null)
            {
                _logger.LogWarning($"Category {id} doesn't exist");
                return NotFound("Category not found");
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync(Category category) {
            if (category is null)
            {
                _logger.LogWarning($"Couldn't add category due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryAsync(int id, Category category)
        {
            //var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (id != category.CategoryId)
            {
                _logger.LogWarning($"Couldn't update category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
            {
                _logger.LogWarning($"Couldn't delete category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            _context.categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
