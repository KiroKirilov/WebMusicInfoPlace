using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Songs;

namespace WMIP.Services
{
    public class SongsService : ISongsService
    {
        private readonly WmipDbContext context;

        public SongsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool Create(CreateSongDto creationInfo)
        {
            try
            {
                var song = new Song
                {
                    Name = creationInfo.Name,
                    Genre = creationInfo.Genre,
                    ReleaseDate = creationInfo.ReleaseDate,
                    ReleaseStage = creationInfo.ReleaseStage,
                    TrackNumber = creationInfo.TrackNumber,
                    MusicVideoLink = creationInfo.MusicVideoLink,
                    Lyrics = creationInfo.Lyrics,
                    ArtistId = creationInfo.ArtistId,
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

        public bool Edit(EditSongDto editInfo)
        {
            try
            {
                var song = this.context.Songs.Find(editInfo.Id);
                if (song == null)
                {
                    return false;
                }

                song.Name = editInfo.Name;
                song.Genre = editInfo.Genre;
                song.ReleaseDate = editInfo.ReleaseDate;
                song.ReleaseStage = editInfo.ReleaseStage;
                song.TrackNumber = editInfo.TrackNumber;
                song.MusicVideoLink = editInfo.MusicVideoLink;
                song.Lyrics = editInfo.Lyrics;
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
            var songs = this.context.Songs.Where(s => s.ArtistId == userId && s.ApprovalStatus == ApprovalStatus.Approved).ToList();
            return songs;
        }

        public bool IsUserCreatorById(string userId, int songId)
        {
            return this.context.Songs.Any(s => s.Id == songId && s.ArtistId == userId);
        }

        public bool IsUserCreatorByName(string username, int songId)
        {
            return this.context.Songs.Any(s => s.Id == songId && s.Artist.UserName == username);
        }
    }
}
