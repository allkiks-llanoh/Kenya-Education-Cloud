using KEC.ECommerce.Data.UnitOfWork;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Pagination;
using KEC.ECommerce.Web.UI.Security.Database;
using KEC.ECommerce.Web.UI.Security.Extensions;
using KEC.ECommerce.Web.UI.Security.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KEC.ECommerce.Web.UI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddTransient(typeof(IPageHelper<>), typeof(PageHelper<>));
            services.AddSingleton<IPageConfig, PageConfig>();
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
            
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
           
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
              
                options.User.RequireUniqueEmail = true;
            });
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddTransient<IUnitOfWork>(m => new EFUnitOfWork());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseSession();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
