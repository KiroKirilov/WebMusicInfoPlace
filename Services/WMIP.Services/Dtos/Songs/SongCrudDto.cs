using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Dtos.Songs
{
    public class SongCrudDto
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ReleaseStage ReleaseStage { get; set; }

        public string MusicVideoLink { get; set; }

        public int TrackNumber { get; set; }

        public string Lyrics { get; set; }
    }
}
