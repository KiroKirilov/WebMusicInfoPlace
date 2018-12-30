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

        public bool Delete(int songId)
        {
            try
            {
                var song = this.context.Songs.Find(songId);
                this.context.Songs.Remove(song);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(int sondId, string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, int trackNumber, string mvLink, string lyrics)
        {
            try
            {
                var song = this.context.Songs.Find(sondId);
                if (song == null)
                {
                    return false;
                }

                song.Name = name;
                song.Genre = genre;
                song.ReleaseDate = releaseDate;
                song.ReleaseStage = releaseStage;
                song.TrackNumber = trackNumber;
                song.MusicVideoLink = mvLink;
                song.Lyrics = lyrics;
                song.ApprovalStatus = ApprovalStatus.Pending;
                song.AlbumsSongs.Clear();

                this.context.Songs.Update(song);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Song> GetAllSongsByUser(string userId)
        {
            try
            {
                return this.context.Songs.Where(s => s.ArtistId == userId).ToList();
            }
            catch
            {
                return new List<Song>();
            }
        }

        public Song GetById(int songId)
        {
            return this.context.Songs.Find(songId);
        }

        public Song GetNotSecretById(int id)
        {
            var song = this.context.Songs.FirstOrDefault(s => s.Id == id && s.ReleaseStage != ReleaseStage.Secret && s.ApprovalStatus == ApprovalStatus.Approved);
            return song;
        }

        public IEnumerable<Song> GetUsersApprovedSongs(string userId)
        {
            var songs = this.context.Songs.Where(s => s.ArtistId == userId).ToList();
            return songs;
        }

        public bool IsUserCreator(string userId, int songId)
        {
            var song = this.context.Songs.FirstOrDefault(s => s.Id == songId && s.ArtistId == userId);

            return song != null;
        }
    }
}
