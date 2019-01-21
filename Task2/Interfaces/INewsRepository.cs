using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;

namespace News_portal.Interfaces
{
    public interface INewsRepository :IDisposable 
    {
        IEnumerable<News> GetAllNews();

        IEnumerable<News> FindNews(Func<News, bool> predicate);

        News GetNewsById(int id);

        int GetNewsId(News news);

        int Count(Func<News, bool> predicate);

        void Create(News news);

        void Update(News news);

        void Delete(News news);

    }
}
