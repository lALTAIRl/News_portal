using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News_portal.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
       
        public string Caption { get; set; }
        
        public string Description { get; set; }
        
        public string Text { get; set; }
        
        public string ImageURL { get; set; }
    }
}
