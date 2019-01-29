using News_portal.DAL.Data;
using News_portal.DAL.Interfaces;
using News_portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace News_portal.DAL.Repositories
{
    public class NewsRepository : INewsRepository
    {
        protected readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _context.Set<News>().ToListAsync();
        }

        public async Task<IQueryable<News>> SelectNewsAsync(Func<News, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<News>().Where(predicate).AsQueryable();
            });
        }

        public async Task<IEnumerable<News>> FindNewsAsync(Func<News, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<News>().Where(predicate);
            });
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _context.Set<News>().FindAsync(id);
        }

        public async Task<int> CountNewsAsync(Func<News, bool> predicate)
        {
            return await Task.Run(() =>
            {
                return _context.Set<News>().Where(predicate).Count();
            });
        }

        public async Task CreateNewsAsync(News news)
        {
            await _context.AddAsync(news);
            await Save();
        }

        public async Task UpdateNewsAsync(News news)
        {
            await Task.Run(() =>
            {
                _context.Entry(news).State = EntityState.Modified;
            });
            await Save();
        }

        public async Task DeleteNewsAsync(News news)
        {
            _context.Remove(news);
            await Save();
        }

        public async Task<List<News>> GetUsersFavouritesAsync(string id)
        {
                var news = await _context.NewsCollection.Where(u => u.NewsApplicationUsers.Select(x=>x.ApplicationUserId).Contains(id)).ToListAsync();     
                return news;      
        }

        public async Task RemoveNewsFromUserFavourites(int newsId, string userId)
        {
            var favouriteNews = await _context.FindAsync<NewsApplicationUser>(newsId, userId);
            if (favouriteNews != null)
            {
                _context.Remove(favouriteNews);
                await Save();
            }
        }

        public async Task AddNewsToUserFavourites(int newsId, string userId)
        {
            var news = await _context.NewsCollection.SingleAsync(n => n.Id == newsId);
            var favouriteNews = new NewsApplicationUser
            {
                NewsId = newsId,
                FavouriteNews = news,
                ApplicationUserId = userId,
                ApplicationUserFavourited = await _context.Users.SingleAsync(u=>u.Id==userId)
            };
            if (await _context.FindAsync<NewsApplicationUser>(newsId, userId) == null)
            {
                _context.Add(favouriteNews);
                await Save();
            }
        }

    }
}
