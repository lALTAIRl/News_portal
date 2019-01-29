using News_portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News_portal.BLL.DTO
{
    public class NewsApplicationUserDTO
    {
        public int NewsId { get; set; }

        public News FavouriteNews { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUserFavourited { get; set; }
    }
}
