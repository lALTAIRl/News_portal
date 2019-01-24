using Microsoft.EntityFrameworkCore;
using News_portal.Data;
using News_portal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_portal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected async Task Save() => await _context.SaveChangesAsync();
   
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IQueryable<T>> SelectAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().Where(predicate).AsQueryable();
            });
        }

        public async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().Where(predicate);
            });
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> CountAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<T>().Where(predicate).Count();
            });
        }

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await Save();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
             {
                 _context.Entry(entity).State = EntityState.Modified; 
             });
            await Save();
        }

        public async Task DeleteAsync(T entity)
        {          
            _context.Remove(entity);
            await Save();
        }
    }
}
