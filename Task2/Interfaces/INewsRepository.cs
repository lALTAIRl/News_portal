using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;

namespace News_portal.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        Task<List<News>> GetUsersFavouritesAsync(string id);

        Task RemoveNewsFromUserFavourites(int newsId, string userId);

        Task AddNewsToUserFavourites(int newsId, string userId);
    }
}
