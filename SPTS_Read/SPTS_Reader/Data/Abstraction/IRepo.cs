using MongoDB.Driver;
using System.Linq.Expressions;

namespace SPTS_Reader.Data.Abstraction
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
