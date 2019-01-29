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
        Task<IEnumerable<NewsDTO>> GetAllNewsDTOAsync();

        Task<IEnumerable<NewsDTO>> FindNewsDTOAsync(Func<NewsDTO, bool> predicate);

        Task<IQueryable<NewsDTO>> SelectNewsDTOAsync(Func<NewsDTO, bool> predicate);

        Task<NewsDTO> GetNewsDTOByIdAsync(int id);

        Task CreateNewsDTOAsync(NewsDTO newsDTO);

        Task UpdateNewsDTOAsync(NewsDTO newsDTO);

        Task DeleteNewsDTOAsync(NewsDTO newsDTO);

        Task<int> CountNewsDTOAsync(Func<NewsDTO, bool> predicate);

        Task<List<NewsDTO>> GetUsersFavouritesDTOAsync(string id);

        Task RemoveNewsDTOFromUserFavourites(int newsDTOId, string userId);

        Task AddNewsDTOToUserFavourites(int newsDTOId, string userId);
    }
}
