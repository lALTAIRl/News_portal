using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class NewsInitializer
    {
        public static async Task NewsInitializeAsync()
        {
            string firstNewsCaption = "test news";
            string firstNewsText = "<b>first test news<b>";
            string firstNewsImageUrl = "https://dpchas.com.ua/sites/default/files/u85/22_27.jpg";
            DateTime firstNewsDateOfCreating = DateTime.Now;
            bool firstNewsIsPublished = true;
            DateTime firstNewsDateOfPublishing = DateTime.Now;

            News firstNews = new News
            {
                Caption = firstNewsCaption,
                Text = firstNewsText,
                ImageURL = firstNewsImageUrl,
                DateOfCreating = firstNewsDateOfCreating,
                IsPublished = firstNewsIsPublished,
                DateOfPublishing = firstNewsDateOfPublishing
            };
        }
    }
}
