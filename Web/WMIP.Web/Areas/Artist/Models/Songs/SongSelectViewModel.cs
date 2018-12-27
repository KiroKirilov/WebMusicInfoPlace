using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Areas.Artist.Models.Songs
{
    public class SongSelectViewModel : IMapFrom<Song>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
