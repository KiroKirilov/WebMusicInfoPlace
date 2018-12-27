using System;
using System.Collections.Generic;
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

                foreach (var songId in selectedSongIds)
                {
                    var currentAlbumSong = new AlbumSong
                    {
                        SongId = songId,
                    };

                    album.AlbumsSongs.Add(currentAlbumSong);
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
