using CardShop.Context;
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
        public CategoryController(CardShopDbContext context) {
            _context = context;
        }

        [HttpGet("CategoryProducts")]
        public  async Task<ActionResult<IEnumerable<Category>>> GetProductsInCategoryAsync()
        {
            var category = await _context.categories.Include(p=> p.Products).ToListAsync();
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryAsync()
        {
            var category = await _context.categories.ToListAsync();
            if (category is null) 
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id) 
        {
            var category = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync(Category category) {
            if (category is null)
                return NotFound();

            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id, Category category)
        {
            //var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (id != category.CategoryId)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
                return NotFound();

            _context.categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
