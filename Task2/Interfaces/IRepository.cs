using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_portal.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);

        Task<IQueryable<T>> SelectAsync(Func<T, bool> predicate);

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<int> CountAsync(Func<T, bool> predicate);
    }
}
