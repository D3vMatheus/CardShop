using CardShop.Context;
using CardShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CardShopDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(CardShopDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var product = await _context.products.AsNoTracking().Take(10).ToListAsync();

            if(product is null)
            {
                _logger.LogWarning("Products doesn't exist");
                return NotFound();
            }

            return product;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.ProductId == id);
            
            if(product is null)
            {
                _logger.LogWarning($"Product {id} doesn't exist");
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult> AddProductAsync(Product product)
        {
            if(product is null)
            {
                _logger.LogWarning($"Couldn't add product due invalid information detected");
                return BadRequest();
            }

            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProductById", new { id = product.ProductId }, product);
        }

        //This approach only allow full update products
        //It's possible bypass this approach using PATCH or a different PUT implementation
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(int id, Product product)
        {
            if(id != product.ProductId)
            {
                _logger.LogWarning($"Couldn't update product due invalid information detected: {id} doesn't exist");
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null)
            {
                _logger.LogWarning($"Couldn't delete product due invalid information detected: {id} doesn't exist");
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
