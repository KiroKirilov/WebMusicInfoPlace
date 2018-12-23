using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class Song : BaseModel<int>
    {
        public Song()
            : base()
        {
            this.SongAlbums = new HashSet<AlbumSong>();
        }

        public string Name { get; set; }

        public string MusicVideoLink { get; set; }

        public string Lyrics { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ArtistId { get; set; }
        public virtual User Artist { get; set; }

        public virtual ICollection<AlbumSong> SongAlbums { get; set; }
    }
}
