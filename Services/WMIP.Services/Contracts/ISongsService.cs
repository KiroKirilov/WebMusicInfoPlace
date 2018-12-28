using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Contracts
{
    public interface ISongsService
    {
        bool CreateNew(string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, int trackNumber, string mvLink, string lyrics, string artistId);

        IEnumerable<Song> GetUsersApprovedSongs(string userId);

        Song GetById(int songId);
    }
}
