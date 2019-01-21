using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.Models;

namespace News_portal.Interfaces
{
    public interface INewsRepository<News> : IRepository<T> where T
    {
        

    }
}
