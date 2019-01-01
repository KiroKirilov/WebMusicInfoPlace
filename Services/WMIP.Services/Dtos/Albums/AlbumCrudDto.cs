using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Dtos.Albums
{
    public class AlbumCrudDto
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string SpotifyLink { get; set; }

        public string AlbumCoverLink { get; set; }
        
        public ReleaseStage ReleaseStage { get; set; }

        public IEnumerable<int> SelectedSongIds { get; set; }
    }
}
