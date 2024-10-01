using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAsync();
        Task<IEnumerable<Category>> GetProductsInCategoryAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<Category> DeleteAsync(int id);
    }
}
