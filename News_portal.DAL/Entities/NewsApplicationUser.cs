using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_portal.DAL.Entities
{
    public class NewsApplicationUser
    {
        public int NewsId { get; set; }

        public News FavouriteNews { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUserFavourited { get; set; }
    }
}
