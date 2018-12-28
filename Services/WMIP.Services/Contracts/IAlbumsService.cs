using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Contracts
{
    public interface IAlbumsService
    {
        bool CreateNew(string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, string spotifyLink, string albumCoverLink, IEnumerable<int> selectedSongIds,string artistId);

        bool Edit(int albumId, string name, string genre, DateTime? releaseDate, ReleaseStage releaseStage, string spotifyLink, string albumCoverLink, IEnumerable<int> selectedSongIds);

        bool IsUserCreator(string userId, int albumId);

        bool DoesAlbumExist(int albumId, string albumName);

        Album GetById(int id);
    }
}
