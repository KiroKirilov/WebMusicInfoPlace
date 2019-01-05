using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Posts;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Posts.Extensions;

namespace WMIP.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly WmipDbContext context;

        public ArticlesService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool Create(CreatePostDto creationInfo)
        {
            try
            {
                var article = new Article
                {
                    Title = creationInfo.Title,
                    Body = creationInfo.Body,
                    Summary = creationInfo.Summary,
                    UserId = creationInfo.UserId,
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

        public bool Edit(EditPostDto editInfo)
        {
            try
            {
                var article = this.context.Articles.Find(editInfo.Id);
                if (article == null)
                {
                    return false;
                }
                article.Title = editInfo.Title;
                article.Body = editInfo.Body;
                article.Summary = editInfo.Summary;
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

        public IEnumerable<UserRatedPostDto> GetAllOrderedByDate(string username)
        {
            return this.context.Articles
                 .ToList()
                 .Select(a => a.ToDto(username))
                 .OrderByDescending(a => a.CreatedOn)
                 .ToList();
        }

        public UserRatedPostDto GetById(int articleId, string username)
        {
            var article = this.context.Articles.Find(articleId);
            if (article == null)
            {
                return null;
            }

            return article.ToDto(username);
        }

        public Article GetById(int id)
        {
            return this.context.Articles.Find(id);
        }

        public IEnumerable<UserRatedPostDto> GetLatest(int count, string username)
        {
            return this.context.Articles
                .OrderByDescending(a => a.CreatedOn)
                .Take(count)
                .ToList()
                .Select(a => a.ToDto(username));
        }
    }
}

