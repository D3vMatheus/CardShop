using CardShop.Context;
using CardShop.Filters;
using CardShop.Model;
using CardShop.Repository;
using CardShop.Repository.Interfaces;
using CardShop.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository repository, ILogger<CategoryController> logger) {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("CategoryProducts")]
        public  async Task<ActionResult<IEnumerable<Category>>> GetProductsInCategory()
        {
            var category = await _repository.GetProductsInCategoryAsync();

            if (category is null)
            {
                _logger.LogWarning($"Category doesn't exist");
                return NotFound("Category not found");
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var categories = await _repository.GetAsync();

            if (categories is null)
            {
                _logger.LogWarning("Category doesn't exist");
                return NotFound("Category not found");
            }

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id) 
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null)
            {
                _logger.LogWarning($"Category {id} doesn't exist");
                return NotFound("Category not found");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category) {
            
            if (category is null)
            {
                _logger.LogWarning($"Couldn't add category due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            var newCategory = await _repository.CreateAsync(category);

            return new CreatedAtRouteResult("GetCategoryById", new { id = newCategory.CategoryId}, newCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning($"Couldn't update category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            await _repository.UpdateAsync(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = _repository.GetByIdAsync(id);
            if (category is null)
            {
                _logger.LogWarning($"Couldn't delete category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
