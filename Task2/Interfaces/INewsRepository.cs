using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;

namespace News_portal.Interfaces
{
    public interface INewsRepository<T> :IDisposable 
    {

        Task<News> GetNewsById(int newsId);

        Task<News> CreateNews(News news);

        Task<News> UpdateNews(/*model*/);

        Task<News> DeleteNews(/*model*/);

    }
}
