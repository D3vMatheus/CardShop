using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetProductsInCategoryAsync();
        Task<Category> DeleteAsync(int id);
    }
}
