using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WMIP.Constants.Enums;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class ApprovalServiceTests : BaseTestClass
    {
        [Fact]
        public void GetAllItemsForApproval_ReturnsCorrectAmountOfItems()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var pendingSong = new Song { ApprovalStatus = ApprovalStatus.Pending };
            var approvedSong = new Song { ApprovalStatus = ApprovalStatus.Approved };
            var rejectedSong = new Song { ApprovalStatus = ApprovalStatus.Rejected };
            var pendingAlbum = new Album { ApprovalStatus = ApprovalStatus.Pending };
            var approvedAlbum = new Album { ApprovalStatus = ApprovalStatus.Approved };
            var rejectedAlbum = new Album { ApprovalStatus = ApprovalStatus.Rejected };
            context.Songs.AddRange(pendingSong, approvedSong, rejectedSong);
            context.Albums.AddRange(pendingAlbum, approvedAlbum, rejectedAlbum);
            context.SaveChanges();
            var approvalService = new ApprovalService(context);

            // Act
            var result = approvalService.GetAllItemsForApproval();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetUsersApprovalRequests_GetCorrectItems()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var user1 = new User() { Id = "1" };
            var user2 = new User() { Id = "2" };
            var pendingSong = new Song { ApprovalStatus = ApprovalStatus.Pending, ArtistId = user1.Id };
            var approvedSong = new Song { ApprovalStatus = ApprovalStatus.Approved, ArtistId = user1.Id };
            var rejectedSong = new Song { ApprovalStatus = ApprovalStatus.Rejected, ArtistId = user2.Id };
            var pendingAlbum = new Album { ApprovalStatus = ApprovalStatus.Pending, ArtistId = user2.Id };
            var approvedAlbum = new Album { ApprovalStatus = ApprovalStatus.Approved, ArtistId = user1.Id };
            var rejectedAlbum = new Album { ApprovalStatus = ApprovalStatus.Rejected, ArtistId = user1.Id };
            context.Songs.AddRange(pendingSong, approvedSong, rejectedSong);
            context.Albums.AddRange(pendingAlbum, approvedAlbum, rejectedAlbum);
            context.SaveChanges();
            var approvalService = new ApprovalService(context);

            // Act
            var results = approvalService.GetUsersApprovalRequests(user1.Id);

            // Assert
            Assert.Equal(4, results.Count());
            Assert.True(results.All(r => r.ArtistId == user1.Id));
        }

        [Fact]
        public void ChangeApprovalStatus_ChangesToCorrectStatusForCorrectItem()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var user1 = new User() { Id = "1" };
            var user2 = new User() { Id = "2" };
            var pendingSong = new Song { ApprovalStatus = ApprovalStatus.Pending, ArtistId = user1.Id };
            var approvedSong = new Song { ApprovalStatus = ApprovalStatus.Approved, ArtistId = user1.Id };
            var rejectedSong = new Song { ApprovalStatus = ApprovalStatus.Rejected, ArtistId = user2.Id };
            var pendingAlbum = new Album { ApprovalStatus = ApprovalStatus.Pending, ArtistId = user2.Id };
            var approvedAlbum = new Album { ApprovalStatus = ApprovalStatus.Approved, ArtistId = user1.Id };
            var rejectedAlbum = new Album { ApprovalStatus = ApprovalStatus.Rejected, ArtistId = user1.Id };
            context.Songs.AddRange(pendingSong, approvedSong, rejectedSong);
            context.Albums.AddRange(pendingAlbum, approvedAlbum, rejectedAlbum);
            context.SaveChanges();
            var approvalService = new ApprovalService(context);

            // Act
            approvalService.ChangeApprovalStatus(pendingSong.Id, ActionItemType.Song, ApprovalStatus.Approved);
            approvalService.ChangeApprovalStatus(pendingAlbum.Id, ActionItemType.Album, ApprovalStatus.Rejected);

            // Assert
            Assert.Equal(ApprovalStatus.Approved, pendingSong.ApprovalStatus);
            Assert.Equal(ApprovalStatus.Rejected, pendingAlbum.ApprovalStatus);
        }
    }
}
