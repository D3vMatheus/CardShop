using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetProductsInCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<Category> DeleteAsync(int id);
    }
}
