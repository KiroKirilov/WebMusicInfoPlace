using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Albums;

namespace WMIP.Services.Contracts
{
    public interface IAlbumsService : IUserCreatedEntityService, ICrudableEntityService<CreateAlbumDto, EditAlbumDto, Album, int>
    {
        bool DoesAlbumExist(int albumId, string albumName);

        Album GetNotSecretById(int id);

        IEnumerable<Album> GetLatestReleases(int count);

        IEnumerable<Album> GetComingSoonReleases(int count);

        IEnumerable<ScoredAlbumDto> GetMostAcclaimed(ReviewType reviewType, int count);

        IEnumerable<Album> GetAllAlbumsByUser(string userId);
    }
}
