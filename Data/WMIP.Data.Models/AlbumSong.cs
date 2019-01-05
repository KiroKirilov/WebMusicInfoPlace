using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class AlbumSong : BaseModel<int>
    {
        public AlbumSong()
            : base()
        { }

        public int SongId { get; set; }
        public virtual Song Song { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
