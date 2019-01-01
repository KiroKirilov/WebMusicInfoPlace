using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Albums
{
    public class CreateAlbumDto : AlbumCrudDto
    {
        public string ArtistId { get; set; }
    }
}
