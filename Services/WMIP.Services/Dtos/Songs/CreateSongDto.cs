using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Songs
{
    public class CreateSongDto : SongCrudDto
    {
        public string ArtistId { get; set; }
    }
}
