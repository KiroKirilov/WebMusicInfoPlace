using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WMIP.Automapper;
using WMIP.Constants;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services;
using WMIP.Services.Contracts;
using WMIP.Web.Middlewares.Extensions;

namespace WMIP.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure db context
            services.AddDbContext<WmipDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            // Configure services
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<ISongsService, SongsService>();
            services.AddTransient<IApprovalService, ApprovalService>();
            services.AddTransient<IAlbumsService, AlbumsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IRatingsService, RatingsService>();

            // Configure AutoMapper
            var mapperConfigBuilder = new MapperConfigBuilder();
            var config = mapperConfigBuilder.Execute(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(ISearchService)));
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Change password requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = PasswordConstants.RequireConfirmedEmail;
                options.Password.RequireLowercase = PasswordConstants.RequireLowercase;
                options.Password.RequireUppercase = PasswordConstants.RequireUppercase;
                options.Password.RequireNonAlphanumeric = PasswordConstants.RequireNonAlphanumeric;
                options.Password.RequiredLength = PasswordConstants.RequiredLength;
                options.Password.RequiredUniqueChars = PasswordConstants.RequiredUniqueChars;
                options.Password.RequireDigit = PasswordConstants.RequireDigit;
            });

            // Configure identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WmipDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Users/Login");
                options.LogoutPath = new PathString("/Users/Logout");
                options.AccessDeniedPath = new PathString("/Users/AccessDenied");
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSeedRoles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
