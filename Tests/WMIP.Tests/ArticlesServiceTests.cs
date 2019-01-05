using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Services.Dtos.Posts;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class ArticlesServiceTests : BaseTestClass
    {
        [Fact]
        public void GetById_ReturnCorrectArticle()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article1 = new Article { Id = 1, Title = "ar1", Ratings = new List<Rating>() };
            var article2 = new Article { Id = 2, Title = "art2", Ratings = new List<Rating>() };
            var article3 = new Article { Id = 3, Title = "art3", Ratings = new List<Rating>() };
            context.Articles.AddRange(article1, article2, article3);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);

            // Act
            var result = articlesService.GetById(1);
            var resultDto = articlesService.GetById(1, "");

            //Assert
            Assert.Equal("ar1", result.Title);
            Assert.Equal("ar1", resultDto.Title);
            Assert.Equal(RatingType.Neutral, resultDto.CurrentUserRating);
        }

        [Fact]
        public void GetAll_ReturnsCorrectAmountOfItems()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article1 = new Article { Id = 1, Title = "ar1" };
            var article2 = new Article { Id = 2, Title = "art2" };
            var article3 = new Article { Id = 3, Title = "art3" };
            context.Articles.AddRange(article1, article2, article3);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);

            // Act
            var result = articlesService.GetAll().ToList();

            //Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetAllOrderedByDate_ReturnsItemsInCorrectOrder()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article1 = new Article { Id = 1, Title = "ar1", User = new User { UserName = "aaa" }, Ratings = new List<Rating>() };
            var article2 = new Article { Id = 2, Title = "art2", User = new User { UserName = "aaa" }, Ratings = new List<Rating>() };
            var article3 = new Article { Id = 3, Title = "art3", User = new User { UserName = "aaa" }, Ratings = new List<Rating>() };
            context.Articles.AddRange(article1, article2, article3);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);

            // Act
            var result = articlesService.GetAllOrderedByDate("aaa").ToList();

            //Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetLatest_ReturnsCorrectArticles()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article1 = new Article { Id = 1, Title = "ar1", User = new User { UserName = "aaa" }, Ratings = new List<Rating>(), CreatedOn = DateTime.UtcNow.AddDays(-1) };
            var article2 = new Article { Id = 2, Title = "art2", User = new User { UserName = "aaa" }, Ratings = new List<Rating>(), CreatedOn = DateTime.UtcNow.AddDays(-2) };
            var article3 = new Article { Id = 3, Title = "art3", User = new User { UserName = "aaa" }, Ratings = new List<Rating>(), CreatedOn = DateTime.UtcNow.AddDays(1) };
            context.Articles.AddRange(article1, article2, article3);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);

            // Act
            var results = articlesService.GetLatest(1, "aaa");

            //Assert
            Assert.Single(results);
            Assert.Equal(3, results.First().Id);
        }

        [Fact]
        public void Create_IncreasesCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var articlesService = new ArticlesService(context);
            var creationInfo = new CreatePostDto() { Title = "article1" };

            // Act
            articlesService.Create(creationInfo);

            //Assert
            Assert.Single(context.Articles);
        }

        [Fact]
        public void Edit_ChangesProperties()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article = new Article { Id = 1, Title = "art1" };
            context.Articles.Add(article);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);
            var editInfo = new EditPostDto() { Id = 1, Title = "aaaararar" };

            // Act
            articlesService.Edit(editInfo);

            //Assert
            Assert.Equal(editInfo.Title, context.Articles.First().Title);
        }

        [Fact]
        public void Delete_RemovesCorrectAlbum()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var article1 = new Article { Id = 1, Title = "s1" };
            var article2 = new Article { Id = 2, Title = "s2" };
            context.Articles.AddRange(article1, article2);
            context.SaveChanges();
            var articlesService = new ArticlesService(context);

            // Act
            articlesService.Delete(1);

            //Assert
            Assert.Single(context.Articles);
            Assert.Equal(article2.Title, context.Articles.First().Title);
        }
    }
}
