using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class RatingServiceTests : BaseTestClass
    {
        [Fact]
        public void Rate_CreatesNewRatingIfItDoesntAlreadyExist()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var ratingService = new RatingsService(context);

            // Act
            var newRating = ratingService.Rate(1, "4", RatingType.Positive);

            //Assert
            Assert.Equal(1, newRating);
            Assert.Single(context.Ratings);
            Assert.Equal(RatingType.Positive, context.Ratings.First().RatingType);
        }

        [Fact]
        public void Rate_SetsToNeutralIfTheSaveRatingIsGivenTwice()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var ratingService = new RatingsService(context);

            // Act
            ratingService.Rate(1, "4", RatingType.Positive);
            var newRating = ratingService.Rate(1, "4", RatingType.Positive);

            //Assert
            Assert.Equal(0, newRating);
            Assert.Equal(RatingType.Neutral, context.Ratings.First().RatingType);
        }

        [Fact]
        public void Rate_SetsToNewRatingIfARatingAlreadyExists()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var ratingService = new RatingsService(context);

            // Act
            ratingService.Rate(1, "4", RatingType.Positive);
            var newRating = ratingService.Rate(1, "4", RatingType.Negative);

            //Assert
            Assert.Equal(-1, newRating);
            Assert.Equal(RatingType.Negative, context.Ratings.First().RatingType);
        }

        [Fact]
        public void GetUsersRatingTypeForAPost_GetsCorrectRatingType()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var user = new User { Id = "4", UserName = "pesho" };
            context.Users.Add(user);
            context.SaveChanges();
            var ratingService = new RatingsService(context);

            // Act
            ratingService.Rate(1, user.Id, RatingType.Positive);
            var ratingType = ratingService.GetUsersRatingTypeForAPost(1, user.UserName);

            //Assert
            Assert.Equal(RatingType.Positive, ratingType);
        }

        [Fact]
        public void GetUsersRatings_ReturnsCorrectRatings()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var user1 = new User { Id = "4", UserName = "pesho" };
            var user2 = new User { Id = "2", UserName = "ivan" };
            context.Users.AddRange(user1, user2);
            context.SaveChanges();
            var ratingService = new RatingsService(context);

            // Act
            ratingService.Rate(1, user1.Id, RatingType.Positive);
            ratingService.Rate(1, user2.Id, RatingType.Negative);
            var ratings = ratingService.GetUsersRatings(user1.UserName);

            //Assert
            Assert.Equal(RatingType.Positive, ratings.First().RatingType);
        }
    }
}
