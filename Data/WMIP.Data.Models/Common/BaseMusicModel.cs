using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Enums;

namespace WMIP.Data.Models.Common
{
    public class BaseMusicModel : BaseModel<int>
    {
        public BaseMusicModel()
            :base()
        {
            this.AlbumsSongs = new HashSet<AlbumSong>();
        }

        public string Name { get; set; }

        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public ReleaseStage ReleaseStage { get; set; }

        public string ArtistId { get; set; }
        public virtual User Artist { get; set; }

        public virtual ICollection<AlbumSong> AlbumsSongs { get; set; }
    }
}
