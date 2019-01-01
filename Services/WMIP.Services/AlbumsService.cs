using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Albums;

namespace WMIP.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly WmipDbContext context;

        public AlbumsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool DoesAlbumExist(int albumId, string albumName)
        {
            var album = this.context.Albums.FirstOrDefault(a => a.Name == albumName && a.Id == albumId);

            return album != null;
        }

        public bool Edit(EditAlbumDto editInfo)
        {
            try
            {
                var album = this.context.Albums.Find(editInfo.Id);
                if (album == null)
                {
                    return false;
                }

                album.Name = editInfo.Name;
                album.Genre = editInfo.Genre;
                album.ReleaseDate = editInfo.ReleaseDate;
                album.ReleaseStage = editInfo.ReleaseStage;
                album.SpotifyLink = editInfo.SpotifyLink;
                album.AlbumCoverLink = editInfo.AlbumCoverLink;
                album.ApprovalStatus = ApprovalStatus.Pending;
                album.AlbumsSongs.Clear();

                if (editInfo.SelectedSongIds != null)
                {
                    foreach (var songId in editInfo.SelectedSongIds)
                    {
                        var currentAlbumSong = this.context.AlbumsSongs.FirstOrDefault(s => s.SongId == songId && s.AlbumId == editInfo.Id);
                        if (currentAlbumSong == null)
                        {
                            currentAlbumSong = new AlbumSong
                            {
                                SongId = songId,
                            };
                        }

                        album.AlbumsSongs.Add(currentAlbumSong);
                    }
                }

                this.context.Albums.Update(album);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Album GetById(int id)
        {
            var album = this.context.Albums.Find(id);
            return album;
        }

        public IEnumerable<Album> GetComingSoonReleases(int count)
        {
            return this.context.Albums
                .Where(a => a.ReleaseStage != ReleaseStage.Secret && a.ApprovalStatus == ApprovalStatus.Approved && a.ReleaseDate.HasValue && a.ReleaseDate.Value > DateTime.UtcNow)
                .OrderBy(a => a.ReleaseDate)
                .Take(count)
                .ToList();
        }

        public IEnumerable<ScoredAlbumDto> GetMostAcclaimed(ReviewType reviewType, int count)
        {
            return this.context.Albums
                .Select(a => new ScoredAlbumDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    AverageScore = a.Reviews.Where(r => r.ReviewType == reviewType).Any() ?
                        a.Reviews.Where(r => r.ReviewType == reviewType).Average(r => r.ReviewScore) : 0,
                    ArtistName = a.Artist.UserName
                })
                .OrderByDescending(a => a.AverageScore)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Album> GetLatestReleases(int count)
        {
            return this.context.Albums
                .Where(a => a.ReleaseStage == ReleaseStage.Released && a.ApprovalStatus == ApprovalStatus.Approved && a.ReleaseDate.HasValue && a.ReleaseDate.Value <= DateTime.UtcNow)
                .OrderByDescending(a => a.ReleaseDate)
                .Take(count)
                .ToList();
        }

        public bool IsUserCreatorById(string userId, int albumId)
        {
            return this.context.Albums.Any(a => a.Id == albumId && a.ArtistId == userId);
        }

        public IEnumerable<Album> GetAllAlbumsByUser(string userId)
        {
            try
            {
                return this.context.Albums.Where(a => a.ArtistId == userId);
            }
            catch
            {
                return new List<Album>();
            }
        }

        public bool Delete(int albumId)
        {
            try
            {
                var album = this.context.Albums.Find(albumId);
                if (album == null)
                {
                    return false;
                }
                foreach (var review in album.Reviews)
                {
                    review.AlbumId = null;
                    this.context.Update(review);
                }
                foreach (var albumSong in album.AlbumsSongs)
                {
                    this.context.AlbumsSongs.Remove(albumSong);
                }
                this.context.Albums.Remove(album);
                this.context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public Album GetNotSecretById(int id)
        {
            var album = this.context.Albums.FirstOrDefault(a => a.Id == id && a.ReleaseStage != ReleaseStage.Secret && a.ApprovalStatus == ApprovalStatus.Approved);
            return album;
        }

        public bool IsUserCreatorByName(string username, int albumId)
        {
            return this.context.Albums.Any(a => a.Id == albumId && a.Artist.UserName == username);
        }

        public bool Create(CreateAlbumDto creationInfo)
        {
            try
            {
                var album = new Album
                {
                    Name = creationInfo.Name,
                    Genre = creationInfo.Genre,
                    ReleaseDate = creationInfo.ReleaseDate,
                    ReleaseStage = creationInfo.ReleaseStage,
                    SpotifyLink = creationInfo.SpotifyLink,
                    AlbumCoverLink = creationInfo.AlbumCoverLink,
                    ArtistId = creationInfo.ArtistId,
                    ApprovalStatus = ApprovalStatus.Pending
                };

                if (creationInfo.SelectedSongIds != null)
                {
                    foreach (var songId in creationInfo.SelectedSongIds)
                    {
                        var currentAlbumSong = new AlbumSong
                        {
                            SongId = songId,
                        };

                        album.AlbumsSongs.Add(currentAlbumSong);
                    }
                }

                this.context.Albums.Add(album);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
