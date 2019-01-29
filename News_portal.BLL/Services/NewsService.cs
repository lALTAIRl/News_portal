using News_portal.BLL.Interfaces;
using News_portal.DAL.Interfaces;
using News_portal.DAL.Entities;
using News_portal.BLL.DTO;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace News_portal.BLL.Services
{

    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        protected async Task Save() => await _newsRepository.Save();

        public async Task<IEnumerable<NewsDTO>> GetAllNewsAsync()
        {
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<News>, List<NewsDTO>>(await _newsRepository.GetAllNewsAsync());
        }

        public async Task<IEnumerable<NewsDTO>> FindNewsAsync(Func<NewsDTO, bool> predicate)
        {
            var funcmapper = new MapperConfiguration(cfg => cfg.CreateMap<Func<NewsDTO, bool>, Func<News, bool>>()).CreateMapper();
            var newsPredicate = funcmapper.Map<Func<NewsDTO, bool>, Func<News, bool>>(predicate);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await _newsRepository.FindNewsAsync(newsPredicate));
        }

        public async Task<IQueryable<NewsDTO>> SelectNewsAsync(Func<NewsDTO, bool> predicate)
        {
            var funcmapper = new MapperConfiguration(cfg => cfg.CreateMap<Func<NewsDTO, bool>, Func<News, bool>>()).CreateMapper();
            var newsPredicate = funcmapper.Map<Func<NewsDTO, bool>, Func<News, bool>>(predicate);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<News>, IQueryable<NewsDTO>>(await _newsRepository.SelectNewsAsync(newsPredicate));
        }

        public async Task<NewsDTO> GetNewsByIdAsync(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<News, NewsDTO>(await _newsRepository.GetNewsByIdAsync(id));
        }

        public async Task CreateNewsAsync(NewsDTO newsDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NewsDTO, News>()).CreateMapper();
            await _newsRepository.CreateNewsAsync(mapper.Map<NewsDTO, News>(newsDTO));
        }

        public async Task UpdateNewsAsync(NewsDTO newsDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NewsDTO, News>()).CreateMapper();
            await _newsRepository.UpdateNewsAsync(mapper.Map<NewsDTO, News>(newsDTO));
        }

        public async Task DeleteNewsAsync(NewsDTO newsDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NewsDTO, News>()).CreateMapper();
            await _newsRepository.DeleteNewsAsync(mapper.Map<NewsDTO, News>(newsDTO));
        }

        public async Task<int> CountNewsAsync(Func<NewsDTO, bool> predicate)
        {
            var funcmapper = new MapperConfiguration(cfg => cfg.CreateMap<Func<NewsDTO, bool>, Func<News, bool>>()).CreateMapper();
            var newsPredicate = funcmapper.Map<Func<NewsDTO, bool>, Func<News, bool>>(predicate);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return await _newsRepository.CountNewsAsync(newsPredicate);
        }

        public async Task<List<NewsDTO>> GetUsersFavouritesAsync(string id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<News>, List<NewsDTO>>(await _newsRepository.GetUsersFavouritesAsync(id));
        }

        public async Task RemoveNewsFromUserFavourites(int newsDTOId, string userId)
        {
            await _newsRepository.RemoveNewsFromUserFavourites(newsDTOId, userId);
        }

        public async Task AddNewsToUserFavourites(int newsDTOId, string userId)
        {
            await _newsRepository.AddNewsToUserFavourites(newsDTOId, userId);
        }

    }
}
