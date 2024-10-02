using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> DeleteAsync(int id);
    }
}
