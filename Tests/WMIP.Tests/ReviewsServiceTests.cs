using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Services.Dtos.Reviews;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class ReviewsServiceTests : BaseTestClass
    {
        [Fact]
        public void GetReviewType_ReturnsCorrectReviewType()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var reviewsService = new ReviewsService(context);
            var criticRolesCollection = new List<string>() { "Admin", "User", "Critic", "Artist" };
            var userRolesCollection = new List<string>() { "Admin", "User", "Artist" };

            // Act
            var userResult = reviewsService.GetReviewType(userRolesCollection);
            var criticResult = reviewsService.GetReviewType(criticRolesCollection);

            //Assert
            Assert.Equal(ReviewType.Critic, criticResult);
            Assert.Equal(ReviewType.User, userResult);
        }

        [Fact]
        public void GetReviewsByUser_ReturnsCorrectReviews()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review1 = new Review { Title = "r1", UserId = "1" };
            var review2 = new Review { Title = "r2", UserId = "2" };
            var review3 = new Review { Title = "r3", UserId = "2" };
            context.Reviews.AddRange(review1, review2, review3);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);

            // Act
            var results = reviewsService.GetReviewsByUser("1");

            //Assert
            Assert.Single(results);
            Assert.Equal(review1.Title, results.First().Title);
        }

        [Fact]
        public void IsUserCreator_ReturnsCorrectResult()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review1 = new Review { Id = 1, Title = "r1", UserId = "1" };
            var review2 = new Review { Id = 2, Title = "r2", UserId = "2" };
            var review3 = new Review { Id = 3, Title = "r3", UserId = "2" };
            context.Reviews.AddRange(review1, review2, review3);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);

            // Act
            var notCreator = reviewsService.IsUserCreator("1", 2);
            var creator = reviewsService.IsUserCreator("1", 1);

            //Assert
            Assert.False(notCreator);
            Assert.True(creator);
        }

        [Fact]
        public void Create_IncreasesCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var reviewsService = new ReviewsService(context);
            var creationInfo = new CreateReviewDto() { Title = "rev" };

            // Act
            reviewsService.Create(creationInfo);

            //Assert
            Assert.Single(context.Reviews);
        }

        [Fact]
        public void Edit_ChangesProperties()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review = new Review { Id = 1, Title = "rrrr" };
            context.Reviews.Add(review);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);
            var editInfo = new EditReviewDto() { Id = 1, Title = "rev1" };

            // Act
            reviewsService.Edit(editInfo);

            //Assert
            Assert.Equal(editInfo.Title, context.Reviews.First().Title);
        }

        [Fact]
        public void Delete_RemovesCorrectAlbum()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review1 = new Review { Id = 1, Title = "s1" };
            var review2 = new Review { Id = 2, Title = "s2" };
            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);

            // Act
            var result = reviewsService.Delete(1);

            //Assert
            Assert.True(result);
            Assert.Single(context.Reviews);
            Assert.Equal(review2.Title, context.Reviews.First().Title);
        }

        [Fact]
        public void GetById_ReturnsCorrectItem()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review1 = new Review { Id = 1, Title = "s1" };
            var review2 = new Review { Id = 2, Title = "s2" };
            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);

            // Act
            var result = reviewsService.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(review1.Title, result.Title);
        }

        [Fact]
        public void Delete_ReturnsFalseIfItemNotFound()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var review1 = new Review { Id = 1, Title = "s1" };
            var review2 = new Review { Id = 2, Title = "s2" };
            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            var reviewsService = new ReviewsService(context);

            // Act
            var result = reviewsService.Delete(3);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Delete_ReturnsFalseOnExcepiton()
        {
            // Arrange
            var reviewsService = new ReviewsService(null);

            // Act
            var result = reviewsService.Delete(1);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetReviewsByUser_ReturnsEmptyCollectionOnException()
        {
            // Arrange
            var reviewsService = new ReviewsService(null);

            // Act
            var result = reviewsService.GetReviewsByUser("1");

            //Assert
            Assert.Empty(result);
        }
    }
}
