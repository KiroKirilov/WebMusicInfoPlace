using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Extensions;
using WMIP.Web.Models.Songs;

namespace WMIP.Web.Models.Albums
{
    public class AlbumDetailsViewModel : AlbumBasicInfoViewModel, IHaveCustomMappings
    {
        public string Genre { get; set; }

        public string ArtistName { get; set; }

        public string SpotifyLink { get; set; }

        public string AlbumCoverLink { get; set; }

        public double UserScore { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public double CriticScore { get; set; }

        public string UserClass => this.UserScore.ToButtonClass();

        public string CriticClass => this.CriticScore.ToButtonClass();

        public IEnumerable<SongBasicInfoViewModel> Songs { get; set; }

        public virtual void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumDetailsViewModel>()
                .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName))
                .ForMember(m => m.UserScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.User).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.User).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.CriticScore, opts => opts.MapFrom(e => 
                    e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.Songs, opts => opts.MapFrom(e => e.AlbumsSongs
                    .Where(s => s.Song.ReleaseStage != ReleaseStage.Secret && s.Song.ApprovalStatus == ApprovalStatus.Approved)
                    .Select(albumSong => albumSong.Song)
                    .OrderBy(s => s.TrackNumber)));
        }
    }
}
