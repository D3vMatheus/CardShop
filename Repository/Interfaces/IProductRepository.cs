using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface IProductRepository //: IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int id);
    }
}
