using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly WmipDbContext context;

        public ApprovalService(WmipDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<BaseMusicModel> GetAllItemsForApproval()
        {
            var allSongs = this.context.Songs.Where(s => s.ApprovalStatus == ApprovalStatus.Pending).ToList().Select(s => s as BaseMusicModel);
            var allAlbums = this.context.Albums.Where(a =>a.ApprovalStatus == ApprovalStatus.Pending).ToList().Select(a => a as BaseMusicModel);
            var allItemsForApproval = allSongs.Concat(allAlbums);
            return allItemsForApproval;
        }

        public bool ChangeApprovalStatus(int itemId, ActionItemType itemType, ApprovalStatus newStatus)
        {
            try
            {
                if (itemType == ActionItemType.Song)
                {
                    var song = this.context.Songs.Find(itemId);
                    if (song != null)
                    {
                        song.ApprovalStatus = newStatus;
                        this.context.SaveChanges();
                        return true;
                    }

                    return false;
                }

                if (itemType == ActionItemType.Album)
                {
                    var album = this.context.Albums.Find(itemId);
                    if (album != null)
                    {
                        album.ApprovalStatus = newStatus;
                        this.context.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public IEnumerable<BaseMusicModel> GetUsersApprovalRequests(string userId)
        {
            var songs = this.context.Songs.Where(s => s.ArtistId == userId).ToList().Select(s => s as BaseMusicModel);
            var albums = this.context.Albums.Where(a => a.ArtistId == userId).ToList().Select(a => a as BaseMusicModel);
            var allItemsForApproval = songs.Concat(albums);
            return allItemsForApproval;
        }
    }
}
