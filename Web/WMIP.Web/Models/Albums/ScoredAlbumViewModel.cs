using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Extensions;
using WMIP.Services.Dtos.Albums;

namespace WMIP.Web.Models.Albums
{
    public class ScoredAlbumViewModel : AlbumBasicInfoViewModel, IMapFrom<ScoredAlbumDto>
    {
        public double AverageScore { get; set; }

        public string ArtistName { get; set; }

        public string ScoreClass => this.AverageScore.ToButtonClass();
    }
}
