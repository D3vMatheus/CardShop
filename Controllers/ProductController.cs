using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var product = await _repository.GetProductsAsync();

            if(product is null)
            {
                _logger.LogWarning("Products doesn't exist");
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if (product is null)
            {
                _logger.LogWarning($"Product {id} doesn't exist");
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (product is null)
            {
                _logger.LogWarning($"Couldn't add product due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            var newProduct = await _repository.CreateProductAsync(product);

            return new CreatedAtRouteResult("GetProductById", new { id = newProduct.ProductId}, newProduct);
        }

        //This approach only allow full update products
        //It's possible bypass this approach using PATCH or a different PUT implementation
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                _logger.LogWarning($"Couldn't update product due invalid information detected: {id} doesn't exist");
                return BadRequest("Invalid information detected");
            }

            var updatedProduct = await _repository.UpdateProductAsync(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if (product is null)
            {
                _logger.LogWarning($"Couldn't delete product due invalid information detected: {id} doesn't exist");
                return NotFound("Product not found");
            }

            await _repository.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
