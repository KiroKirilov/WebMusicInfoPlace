using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Enums;
using WMIP.Web.Areas.Artist.Models.Common;
using WMIP.Web.Areas.Artist.Models.Songs;

namespace WMIP.Web.Areas.Artist.Models.Albums
{
    public class CreateAlbumViewModel : BaseMusicItemCreationViewModel
    {
        [Display(Name = "Link To Spotify Page")]
        public string SpotifyLink { get; set; }

        [Display(Name = "Album Cover Image Link")]
        public string AlbumCoverLink { get; set; }

        public SongSelectViewModel[] AvailableSongs { get; set; }

        [Display(Name= "Songs in the album")]
        public IEnumerable<int> SelectedSongIds { get; set; }
    }
}
