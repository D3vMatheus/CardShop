using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Repository.Services
{
    public class ProductRepository : IProductsRepository
    {
        private readonly CardShopDbContext _context;
        public ProductRepository(CardShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.products.Take(10).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product is null)
                throw new ArgumentNullException(nameof(product));
            
            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product is null) 
                throw new ArgumentNullException(nameof(product));

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product is null)
                throw new ArgumentNullException(nameof(product));
            
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            
            return product;
        }
    }
}
