using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Services.Dtos.Songs;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class SongsServiceTests : BaseTestClass
    {
        [Fact]
        public void GetUsersApprovedSongs_ReturnsCorrectSongs()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, ArtistId = "1", ApprovalStatus = ApprovalStatus.Pending };
            var song2 = new Song { Id = 2, ArtistId = "1", ApprovalStatus = ApprovalStatus.Rejected };
            var song3 = new Song { Id = 3, ArtistId = "1", ApprovalStatus = ApprovalStatus.Approved };
            var song4 = new Song { Id = 4, ArtistId = "2", ApprovalStatus = ApprovalStatus.Approved };
            context.Songs.AddRange(song1, song2, song3, song4);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            var results = songsService.GetUsersApprovedSongs("1");

            //Assert
            Assert.Single(results);
            Assert.Equal(3, results.First().Id);
        }

        [Fact]
        public void GetNotSecretById_ReturnsCorrectSong()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, ArtistId = "1", ApprovalStatus = ApprovalStatus.Rejected };
            var song2 = new Song { Id = 2, ArtistId = "1", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Revealed };
            context.Songs.AddRange(song1, song2);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            var nonExistant = songsService.GetNotSecretById(1);
            var existant = songsService.GetNotSecretById(2);

            //Assert
            Assert.Null(nonExistant);
            Assert.NotNull(existant);
        }

        [Fact]
        public void GetAllSongsByUser_ReturnsCorrectSongs()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, ArtistId = "1", ApprovalStatus = ApprovalStatus.Rejected };
            var song2 = new Song { Id = 2, ArtistId = "2", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Revealed };
            context.Songs.AddRange(song1, song2);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            var results = songsService.GetAllSongsByUser("1");

            //Assert
            Assert.Single(results);
            Assert.Equal(1, results.First().Id);
        }

        [Fact]
        public void Create_IncreasesCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var songsService = new SongsService(context);
            var creationInfo = new CreateSongDto() { Name = "songs" };

            // Act
            songsService.Create(creationInfo);

            //Assert
            Assert.Single(context.Songs);
        }

        [Fact]
        public void Edit_ChangesProperties()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song = new Song { Id = 1, Name = "songs1" };
            context.Songs.Add(song);
            context.SaveChanges();
            var songsService = new SongsService(context);
            var editInfo = new EditSongDto() { Id = 1, Name = "sssoong" };

            // Act
            songsService.Edit(editInfo);

            //Assert
            Assert.Equal(editInfo.Name, context.Songs.First().Name);
        }

        [Fact]
        public void Delete_RemovesCorrectAlbum()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, Name = "s1" };
            var song2 = new Song { Id = 2, Name = "s2" };
            context.Songs.AddRange(song1, song2);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            songsService.Delete(1);

            //Assert
            Assert.Single(context.Songs);
            Assert.Equal(song2.Name, context.Songs.First().Name);
        }

        [Fact]
        public void IsUserCreator_ReturnsCorrectResults()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, Name = "s1", Artist = new User { Id = "1", UserName = "ivan" } };
            var song2 = new Song { Id = 2, Name = "s1", Artist = new User { Id = "2", UserName = "pesho" } };
            context.Songs.AddRange(song1, song2);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            var usernameResultTrue = songsService.IsUserCreatorByName("ivan", 1);
            var usernameResultFalse = songsService.IsUserCreatorByName("ivan", 2);
            var idResultTrue = songsService.IsUserCreatorById("1", 1);
            var idResultFalse = songsService.IsUserCreatorById("1", 2);

            //Assert
            Assert.True(usernameResultTrue);
            Assert.False(usernameResultFalse);
            Assert.True(idResultTrue);
            Assert.False(idResultFalse);
        }

        [Fact]
        public void GetById_ReturnsCorrectItem()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Id = 1, Name = "s1" };
            var song2 = new Song { Id = 2, Name = "s2" };
            context.Songs.AddRange(song1, song2);
            context.SaveChanges();
            var songsService = new SongsService(context);

            // Act
            var result = songsService.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(song1.Name, result.Name);
        }
    }
}
