using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Services.Dtos.Albums;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class AlbumsServiceTests : BaseTestClass
    {
        [Fact]
        public void DoesAlbumExist_ReturnsTrueIfAlbumExists()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album = new Album { Id = 1, Name = "Abm" };
            context.Albums.Add(album);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var exising = albumsService.DoesAlbumExist(1, "Abm");
            var nonExising = albumsService.DoesAlbumExist(2, "Abm");

            //Assert
            Assert.True(exising);
            Assert.False(nonExising);
        }

        [Fact]
        public void GetNotSecretById_ReturnsCorrectAlbums()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm" };
            var album2 = new Album { Id = 2, Name = "Abm", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album3 = new Album { Id = 3, Name = "Abm" };
            context.Albums.AddRange(album1, album2, album3);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var nonExisting = albumsService.GetNotSecretById(1);
            var exising = albumsService.GetNotSecretById(2);

            //Assert
            Assert.Null(nonExisting);
            Assert.NotNull(exising);
        }

        [Fact]
        public void GetLatestReleases_ReturnsCorrectAlbums()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm1", ReleaseDate = DateTime.UtcNow.AddDays(-1) ,ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album2 = new Album { Id = 2, Name = "Abm2", ReleaseDate = DateTime.UtcNow.AddDays(-2), ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album3 = new Album { Id = 3, Name = "Abm3", ReleaseDate = DateTime.UtcNow.AddDays(+1), ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            context.Albums.AddRange(album1, album2, album3);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var results = albumsService.GetLatestReleases(1);

            //Assert
            Assert.Equal(1, results.First().Id);
        }

        [Fact]
        public void GetComingSoonReleases_ReturnsCorrectAlbums()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm1", ReleaseDate = DateTime.UtcNow.AddDays(-1), ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album2 = new Album { Id = 2, Name = "Abm2", ReleaseDate = DateTime.UtcNow.AddDays(-2), ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album3 = new Album { Id = 3, Name = "Abm3", ReleaseDate = DateTime.UtcNow.AddDays(+1), ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            context.Albums.AddRange(album1, album2, album3);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var results = albumsService.GetComingSoonReleases(1);

            //Assert
            Assert.Equal(3, results.First().Id);
        }

        [Fact]
        public void GetMostAcclaimed_ReturnsCorrectAlbums()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm1", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album2 = new Album { Id = 2, Name = "Abm2", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album3 = new Album { Id = 3, Name = "Abm3", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var review1 = new Review { AlbumId = 1, ReviewScore = 10, ReviewType = ReviewType.User };
            var review3 = new Review { AlbumId = 1, ReviewScore = 4, ReviewType = ReviewType.Critic };
            var review2 = new Review { AlbumId = 2, ReviewScore = 4, ReviewType = ReviewType.User };
            var review4 = new Review { AlbumId = 2, ReviewScore = 10, ReviewType = ReviewType.Critic };
            context.Albums.AddRange(album1, album2, album3);
            context.Reviews.AddRange(review1, review2, review3, review4);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var userAcclaimed = albumsService.GetMostAcclaimed(ReviewType.User, 1);
            var criticAcclaimed = albumsService.GetMostAcclaimed(ReviewType.Critic, 1);

            //Assert
            Assert.Equal(1, userAcclaimed.First().Id);
            Assert.Equal(2, criticAcclaimed.First().Id);
        }

        [Fact]
        public void GetAllAlbumsByUser_ReturnsCorrectAlbums()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm1", ArtistId = "1", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album2 = new Album { Id = 2, Name = "Abm2", ArtistId = "2", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            var album3 = new Album { Id = 3, Name = "Abm3", ArtistId = "3", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Announced };
            context.Albums.AddRange(album1, album2, album3);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            var results = albumsService.GetAllAlbumsByUser("1");

            //Assert
            Assert.Equal(1, results.First().Id);
        }

        [Fact]
        public void Create_IncreasesCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var albumsService = new AlbumsService(context);
            var creationInfo = new CreateAlbumDto() { Name = "Alb" };

            // Act
            albumsService.Create(creationInfo);

            //Assert
            Assert.Single(context.Albums);
        }

        [Fact]
        public void Edit_ChangesProperties()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album = new Album { Id = 1, Name = "Abm1" };
            context.Albums.Add(album);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);
            var editInfo = new EditAlbumDto() { Id = 1, Name = "Alb" };

            // Act
            albumsService.Edit(editInfo);

            //Assert
            Assert.Equal(editInfo.Name, context.Albums.First().Name);
        }

        [Fact]
        public void Delete_RemovesCorrectAlbum()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var album1 = new Album { Id = 1, Name = "Abm1" };
            var album2 = new Album { Id = 2, Name = "Abm2" };
            context.Albums.AddRange(album1, album2);
            context.SaveChanges();
            var albumsService = new AlbumsService(context);

            // Act
            albumsService.Delete(1);

            //Assert
            Assert.Single(context.Albums);
            Assert.Equal(album2.Name, context.Albums.First().Name);
        }
    }
}
