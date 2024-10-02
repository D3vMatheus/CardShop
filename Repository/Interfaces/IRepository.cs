using System.Linq.Expressions;

namespace CardShop.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<T?> Create(T entity);
        Task<T?> Update(T entity);
    }
}
