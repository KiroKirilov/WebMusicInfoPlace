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
    public class SongsService : ISongsService
    {
        private readonly WmipDbContext context;

        public SongsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool CreateNew(string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, int trackNumber, string mvLink, string lyrics, string artistId)
        {
            try
            {
                var song = new Song
                {
                    Name = name,
                    Genre = genre,
                    ReleaseDate = releaseDate,
                    ReleaseStage = releaseStage,
                    TrackNumber = trackNumber,
                    MusicVideoLink = mvLink,
                    Lyrics = lyrics,
                    ArtistId = artistId,
                    ApprovalStatus = ApprovalStatus.Pending
                };

                this.context.Songs.Add(song);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Song> GetUsersApprovedSongs(string userId)
        {
            var songs = this.context.Songs.Where(s => s.ArtistId == userId).ToList();
            return songs;
        }
    }
}
