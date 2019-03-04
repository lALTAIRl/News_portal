using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.DAL.Entities;

namespace News_portal.DAL.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();

        Task<IEnumerable<News>> FindNewsAsync(Func<News, bool> predicate);

        Task<IQueryable<News>> SelectNewsAsync(Func<News, bool> predicate);

        Task<News> GetNewsByIdAsync(int id);

        Task CreateNewsAsync(News news);

        Task UpdateNewsAsync(News news);

        Task DeleteNewsAsync(News news);

        Task Save();

        Task<int> CountNewsAsync(Func<News, bool> predicate);

        Task<List<News>> GetUsersFavouritesAsync(string id);

        Task RemoveNewsFromUserFavourites(int newsId, string userId);

        Task AddNewsToUserFavourites(int newsId, string userId);
    }
}
