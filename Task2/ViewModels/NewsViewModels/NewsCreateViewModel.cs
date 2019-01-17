using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News_portal.ViewModels
{
    public class NewsCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Caption { get; set; }

        [Required]
        [StringLength(350, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
