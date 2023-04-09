
using System.Linq.Expressions;

namespace DLL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindByConditionalAsync(Expression<Func<T,bool>> predicate);
    }
}
