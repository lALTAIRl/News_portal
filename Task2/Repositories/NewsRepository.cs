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

        }

        public async Task AddNewsToUserFavourites(int newsId, string userId)
        {

        }

    }
}
