using News_portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News_portal.BLL.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDTO>> GetAllNewsAsync();

        Task<IEnumerable<NewsDTO>> FindNewsAsync(Func<NewsDTO, bool> predicate);

        Task<IQueryable<NewsDTO>> SelectNewsAsync(Func<NewsDTO, bool> predicate);

        Task<NewsDTO> GetNewsByIdAsync(int id);

        Task CreateNewsAsync(NewsDTO newsDTO);

        Task UpdateNewsAsync(NewsDTO newsDTO);

        Task DeleteNewsAsync(NewsDTO newsDTO);

        Task<int> CountNewsAsync(Func<NewsDTO, bool> predicate);

        Task<List<NewsDTO>> GetUsersFavouritesAsync(string id);

        Task RemoveNewsFromUserFavourites(int newsDTOId, string userId);

        Task AddNewsToUserFavourites(int newsDTOId, string userId);
    }
}
