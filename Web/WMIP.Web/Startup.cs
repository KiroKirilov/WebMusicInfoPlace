using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddDbContext<WmipDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUsersService, UsersService>();

            // Configure AutoMapper
            var mapperConfig = new MapperConfig();
            var config = mapperConfig.Execute(Assembly.GetExecutingAssembly());
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Change password requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = PasswordConstants.RequireConfirmedEmail;
                options.Password.RequireLowercase = PasswordConstants.RequireLowercase;
                options.Password.RequireUppercase = PasswordConstants.RequireUppercase;
                options.Password.RequireNonAlphanumeric = PasswordConstants.RequireNonAlphanumeric;
                options.Password.RequiredLength = PasswordConstants.RequiredLength;
                options.Password.RequiredUniqueChars = PasswordConstants.RequiredUniqueChars;
            });

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WmipDbContext>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
