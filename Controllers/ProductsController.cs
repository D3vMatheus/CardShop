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

        public ProductsController(CardShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var product = _context.products.ToList();

            if(product is null)
                return NotFound();

            return product;
        }
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.ProductId == id);
            
            if(product is null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if(product is null) 
                return BadRequest();

            _context.products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProductById", new { id = product.ProductId }, product);
        }

        //This approach only allow full update products
        //It's possible bypass this approach using PATCH or a different PUT implementation
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product product)
        {
            if(id != product.ProductId)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.ProductId == id);

            if (product is null)
                return NotFound();

            _context.products.Remove(product);
            _context.SaveChanges();

            return Ok();
        }
    }
}
