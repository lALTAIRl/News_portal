using Microsoft.EntityFrameworkCore;
using News_portal.Data;
using News_portal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T_portal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected void Save() => _context.SaveChanges();

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> FindAsync(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T GetByIdAsync(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public int CountAsync(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void CreateAsync(T entity)
        {
            _context.Add(entity);
            Save();
        }

        public void UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteAsync(T entity)
        {
            _context.Remove(entity);
            Save();
        }
    }
}
