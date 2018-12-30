using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Web.Areas.Artist.Models.Common;

namespace WMIP.Web.Areas.Artist.Models.Albums
{
    public class MyAlbumViewModel : BaseMusicItemViewModel, IMapFrom<Album>
    {
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
