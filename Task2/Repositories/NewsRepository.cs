using News_portal.Data;
using News_portal.Interfaces;
using News_portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace News_portal.Repositories
{
    public class NewsRepository : INewsRepository
    {

        protected readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected void Save() => _context.SaveChanges();

        public IEnumerable<News> GetAllNews()
        {
            return _context.Set<News>();
        }

        public IEnumerable<News> FindNews(Func<News, bool> predicate)
        {
            return _context.Set<News>().Where(predicate);
        }

        public News GetNewsById(int id)
        {
            return _context.Set<News>().Find(id);
        }

        public int GetNewsId(News news)
        {
            return news.Id;
        }

        public int Count(Func<News, bool> predicate)
        {
            return _context.Set<News>().Where(predicate).Count();
        }

        public void Create(News news)
        {
            _context.Add(news);
            Save();
        }

        public void Update(News news)
        {
            _context.Entry(news).State = EntityState.Modified;
        }

        public void Delete(News news)
        {
            _context.Remove(news);
            Save();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<News> News => throw new NotImplementedException();

       
    }
}
