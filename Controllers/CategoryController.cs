using AutoMapper;
using CardShop.Context;
using CardShop.DTOs;
using CardShop.Filters;
using CardShop.Model;
using CardShop.Repository;
using CardShop.Repository.Interfaces;
using CardShop.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork unitOfWork,
                                  ICategoryRepository categoryRepository,
                                  IMapper mapper,
                                  ILogger<CategoryController> logger) 
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("CategoryProducts")]
        public  async Task<ActionResult<IEnumerable<CategoryDTO>>> GetProductsInCategory()
        {
            var category = await _unitOfWork.CategoryRepository.GetProductsInCategoryAsync();

            if (category is null)
            {
                _logger.LogWarning($"Category doesn't exist");
                return NotFound("Category not found");
            }

            var categoryDto = _mapper.Map<IEnumerable<CategoryDTO>>(category);
            
            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories is null)
            {
                _logger.LogWarning("Category doesn't exist");
                return NotFound("Category not found");
            }

            var categoryDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoryDto);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id) 
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryId == id);
            
            if (category is null)
            {
                _logger.LogWarning($"Category {id} doesn't exist");
                return NotFound("Category not found");
            }

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory(CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                _logger.LogWarning($"Couldn't add category due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            var category = _mapper.Map<Category>(categoryDto);

            var newCategory = await _unitOfWork.CategoryRepository.CreateAsync(category);
            
            await _unitOfWork.CommitAsync();

            var newCategoryDto = _mapper.Map<CategoryDTO>(category);

            return new CreatedAtRouteResult("GetCategoryById", new { id = newCategoryDto.CategoryId }, newCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                _logger.LogWarning($"Couldn't update category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            var category = _mapper.Map<Category>(categoryDto);

            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryId == id);
            if (category is null)
            {
                _logger.LogWarning($"Couldn't delete category due invalid information detected: {id} doesn't exist");
                return NotFound("Category not found");
            }

            await _categoryRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
