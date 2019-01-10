using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.ViewModels
{
    public class NewsCreateViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string ImageURL { get; set; }
    }
}
