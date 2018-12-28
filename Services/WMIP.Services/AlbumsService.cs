using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly WmipDbContext context;

        public AlbumsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool CreateNew(
            string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, string spotifyLink, string albumCoverLink, IEnumerable<int> selectedSongIds, string artistId)
        {
            try
            {
                var album = new Album
                {
                    Name = name,
                    Genre = genre,
                    ReleaseDate = releaseDate,
                    ReleaseStage = releaseStage,
                    SpotifyLink = spotifyLink,
                    AlbumCoverLink = albumCoverLink,
                    ArtistId = artistId,
                    ApprovalStatus = ApprovalStatus.Pending
                };

                if (selectedSongIds != null)
                {
                    foreach (var songId in selectedSongIds)
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

        public bool DoesAlbumExist(int albumId, string albumName)
        {
            var album = this.context.Albums.FirstOrDefault(a => a.Name == albumName && a.Id == albumId);

            return album != null;
        }

        public bool Edit(int albumId, string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, string spotifyLink, string albumCoverLink, IEnumerable<int> selectedSongIds)
        {
            try
            {
                var album = this.context.Albums.Find(albumId);
                if (album == null)
                {
                    return false;
                }

                album.Name = name;
                album.Genre = genre;
                album.ReleaseDate = releaseDate;
                album.ReleaseStage = releaseStage;
                album.SpotifyLink = spotifyLink;
                album.AlbumCoverLink = albumCoverLink;
                album.ApprovalStatus = ApprovalStatus.Pending;
                album.AlbumsSongs.Clear();

                if (selectedSongIds != null)
                {
                    foreach (var songId in selectedSongIds)
                    {
                        var currentAlbumSong = this.context.AlbumsSongs.FirstOrDefault(s => s.SongId == songId && s.AlbumId == albumId);
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

        public bool IsUserCreator(string userId, int albumId)
        {
            var album = this.context.Albums.FirstOrDefault(a => a.Id == albumId && a.ArtistId == userId);

            return album != null;
        }
    }
}
