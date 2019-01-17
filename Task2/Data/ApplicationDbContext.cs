using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News_portal.Models;

namespace News_portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<News> NewsCollection { get; set; }
      //public DbSet<NewsApplicationUser> FavouriteNews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<NewsApplicationUser>()
                .HasKey(key => new { key.NewsId, key.ApplicationUserId });

            //builder.Entity<NewsApplicationUser>()
            //    .HasOne(newsAppUser => newsAppUser.FavouriteNews)
            //    .WithMany(news => news.NewsApplicationUsers)
            //    .HasForeignKey(newsAppUser => newsAppUser.NewsId);

            //builder.Entity<NewsApplicationUser>()
            //    .HasOne(newsAppUser => newsAppUser.ApplicationUserFavourited)
            //    .WithMany(news => news.NewsApplicationUsers)
            //    .HasForeignKey(newsAppUser => newsAppUser.ApplicationUserId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
