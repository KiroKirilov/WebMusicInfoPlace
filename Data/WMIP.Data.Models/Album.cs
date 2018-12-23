using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class Album : BaseModel<int>
    {
        public Album()
            : base()
        {
            this.Reviews = new HashSet<Review>();
            this.AlbumsSongs = new HashSet<AlbumSong>();
        }

        public string Name { get; set; }

        public string SpotifyLink { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string AlbumCoverLink { get; set; }

        public string ArtistId { get; set; }
        public virtual User Artist { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<AlbumSong> AlbumsSongs { get; set; }
    }
}
