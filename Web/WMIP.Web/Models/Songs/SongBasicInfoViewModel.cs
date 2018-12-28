using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Songs
{
    public class SongBasicInfoViewModel : IMapFrom<Song>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TrackNumber { get; set; }
    }
}
