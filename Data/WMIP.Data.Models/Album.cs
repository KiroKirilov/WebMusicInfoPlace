using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Data.Models
{
    public class Album : BaseMusicModel
    {
        public Album()
            : base()
        {
            this.Reviews = new HashSet<Review>();
        }
        
        public string SpotifyLink { get; set; }

        public string AlbumCoverLink { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
