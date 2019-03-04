using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News_portal.DAL.Entities;
using News_portal.Models;

namespace News_portal.ViewModels
{
    public class PageIndexViewModel
    {
        public IEnumerable<News> EnumNews { get; set; }
        public IEnumerable<NewsApplicationUser> EnumFavouriteNews { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
