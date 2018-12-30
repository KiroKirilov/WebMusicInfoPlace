using System;
using System.Linq;
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

        public bool CreateNew(string title, string body, string summary, string userId)
        {
            try
            {
                var article = new Article
                {
                    Title = title,
                    Body = body,
                    Summary = summary,
                    UserId = userId,
                };

                this.context.Articles.Add(article);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int articleId)
        {
            try
            {
                var article = this.context.Articles.Find(articleId);
                if (article == null)
                {
                    return false;
                }
                foreach (var comment in article.Comments)
                {
                    comment.CommentedOnId = null;
                    this.context.Comments.Update(comment);
                }
                this.context.Articles.Remove(article);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(int articleId, string title, string body, string summary)
        {
            try
            {
                var article = this.context.Articles.Find(articleId);
                if (article == null)
                {
                    return false;
                }
                article.Title = title;
                article.Body = body;
                article.Summary = summary;
                this.context.Articles.Update(article);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Article> GetAll()
        {
            return this.context.Articles;
        }

        public Article GetById(int articleId)
        {
            return this.context.Articles.Find(articleId);
        }

        public IEnumerable<Article> GetLatest(int count)
        {
            return this.context.Articles.OrderByDescending(a => a.CreatedOn).Take(count).ToList();
        }
    }
}
