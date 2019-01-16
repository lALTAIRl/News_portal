using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class NewsApplicationUser
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        public News FavouriteNews { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUserFavourited { get; set; }
    }
}
