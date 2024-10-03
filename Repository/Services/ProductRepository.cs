using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Repository.Services
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CardShopDbContext context) : base(context){
        }

        //public async Task<IEnumerable<Product>> GetAsync()
        //{
        //    return await _context.products.Take(10).ToListAsync();
        //}

        //public async Task<Product> GetByIdAsync(int id)
        //{
        //    var product = await _context.products.FirstOrDefaultAsync(c => c.ProductId == id);

        //    if (product is null)
        //        throw new ArgumentNullException(nameof(product));
            
        //    return product;
        //}

        //public async Task<Product> CreateAsync(Product product)
        //{
        //    if (product is null) 
        //        throw new ArgumentNullException(nameof(product));

        //    await _context.products.AddAsync(product);
        //    await _context.SaveChangesAsync();

        //    return product;
        //}

        //public async Task<Product> UpdateAsync(Product product)
        //{
        //    if (product is null)
        //        throw new ArgumentNullException(nameof(product));

        //    _context.Entry(product).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return product;
        //}

        public async Task<Product> DeleteAsync(int id)
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
