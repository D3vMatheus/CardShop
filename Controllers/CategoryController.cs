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
        public ActionResult<IEnumerable<Category>> GetProductsInCategory()
        {
            var category = _context.categories.Include(p=> p.Products).ToList();
            if (category is null)
                return NotFound();

            return category;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            var category = _context.categories.ToList();
            if (category is null) 
                return NotFound();

            return category;
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<Category> GetCategoryById(int id) 
        {
            var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
                return NotFound();

            return category;
        }

        [HttpPost]
        public ActionResult AddCategory(Category category) {
            if (category is null)
                return NotFound();

            _context.categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategoryById", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public ActionResult DeleteCategory(int id, Category category)
        {
            //var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (id != category.CategoryId)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
                return NotFound();

            _context.categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
    }
}
