namespace SPTS_Reader.Data
{
    
    using MongoDB.Driver;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
    }

}
