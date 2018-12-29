using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Data;


namespace Task2.Models
{
    public class NewsInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if(!context.NewsCollection.Any())
            {
                context.NewsCollection.Add(
                    new News
                    {
                        Caption = "test news",
                        Text = "<b>first test news<b>",
                        ImageURL = "https://dpchas.com.ua/sites/default/files/u85/22_27.jpg",
                        DateOfCreating = DateTime.Now,
                        IsPublished = true,
                        DateOfPublishing = DateTime.Now
                    }
                    );
                context.SaveChanges();
            }
        }     
    }
}
