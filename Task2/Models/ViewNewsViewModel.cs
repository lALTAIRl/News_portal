using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Task2.Models
{
    public class ViewNewsViewModel
    {
        News Post { get; set; }

        public ViewNewsViewModel(News post)
        {
            Post = post;
        }
    }
}
