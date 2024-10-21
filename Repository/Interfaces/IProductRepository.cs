using CardShop.Model;
using CardShop.Pagination;

namespace CardShop.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts(ProductsParameters productsParams);
        Task<Product> DeleteAsync(int id);
    }
}
