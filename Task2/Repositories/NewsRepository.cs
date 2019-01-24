using News_portal.Data;
using News_portal.Interfaces;
using News_portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace News_portal.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
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
