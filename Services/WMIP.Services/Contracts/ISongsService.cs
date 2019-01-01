using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Songs;

namespace WMIP.Services.Contracts
{
    public interface ISongsService : IUserCreatedEntityService, ICrudableEntityService<CreateSongDto, EditSongDto, Song, int>
    {
        IEnumerable<Song> GetUsersApprovedSongs(string userId);

        Song GetNotSecretById(int id);

        IEnumerable<Song> GetAllSongsByUser(string userId);
    }
}
