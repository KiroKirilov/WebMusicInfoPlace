using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly WmipDbContext context;

        public ArticlesService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool CreateNew(string title, string body, string userId)
        {
            try
            {
                var article = new Article
                {
                    Title = title,
                    Body = body,
                    UserId = userId,
                };

                this.context.Articles.Add(article);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
