using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Enums;
using WMIP.Web.Areas.Artist.Models.Common;

namespace WMIP.Web.Areas.Artist.Models.Songs
{
    public class SongViewModel : BaseMusicItemViewModel
    {
        [Display(Name = "Music Video Link")]
        public string MusicVideoLink { get; set; }

        public string Lyrics { get; set; }

        [Required]
        [Display(Name = "Track Number")]
        public int TrackNumber { get; set; }
    }
}
