using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WMIP.Automapper;
using WMIP.Constants;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;

namespace WMIP.Tests.Common
{
    public class BaseTestClass
    {
        protected BaseTestClass()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
            services.AddOptions();
            services.AddDbContext<WmipDbContext>(b => b.UseInMemoryDatabase("WmipDbContext").UseInternalServiceProvider(efServiceProvider));
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WmipDbContext>();

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

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            var httpContext = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = httpContext,
                });

            var mapperConfigBuilder = new MapperConfigBuilder();
            var config = mapperConfigBuilder.Execute(Assembly.GetEntryAssembly(), Assembly.GetAssembly(typeof(ISearchService)));
            this.Mapper = config.CreateMapper();

            this.ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IMapper Mapper { get; set; }
    }
}
