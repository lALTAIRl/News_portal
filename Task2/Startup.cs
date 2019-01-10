using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task2.Data;
using Task2.Models;
using Task2.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Task2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });


            //services.AddAuthentication().AddVK(vkoptions =>
            //{
            //    vkoptions.ClientId = Configuration["Authentication:Vk:AppId"];
            //    vkoptions.ClientSecret = Configuration["Authentication:Vk:AppSecret"];

            //    // Request for permissions https://vk.com/dev/permissions?f=1.%20Access%20Permissions%20for%20User%20Token
            //    vkoptions.Scope.Add("email");

            //    // Add fields https://vk.com/dev/objects/user
            //    vkoptions.Fields.Add("uid");
            //    vkoptions.Fields.Add("first_name");
            //    vkoptions.Fields.Add("last_name");


            //    // In this case email will return in OAuthTokenResponse, 
            //    // but all scope values will be merged with user response
            //    // so we can claim it as field
            //    vkoptions.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
            //    vkoptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            //    vkoptions.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
            //    vkoptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");

            //    //vkoptions.ClaimActions.MapJsonKey(ClaimTypes.)
            //});

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
