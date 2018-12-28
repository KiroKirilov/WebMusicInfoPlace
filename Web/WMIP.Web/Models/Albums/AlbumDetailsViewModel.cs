using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Web.Models.Songs;

namespace WMIP.Web.Models.Albums
{
    public class AlbumDetailsViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string ArtistName { get; set; }

        public string SpotifyLink { get; set; }

        public string AlbumCoverLink { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public double UserScore { get; set; }

        public double CriticScore { get; set; }

        public string UserClass => this.UserScore >= 7 ? "btn-success" : this.UserScore >= 4 ? "btn-warning" : "btn-danger";

        public string CriticClass => this.CriticScore >= 7 ? "btn-success" : this.CriticScore >= 4 ? "btn-warning" : "btn-danger";

        public IEnumerable<SongBasicInfoViewModel> Songs { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumDetailsViewModel>()
                .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName))
                .ForMember(m => m.UserScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.User).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.User).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.CriticScore, opts => opts.MapFrom(e => 
                    e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.Songs, opts => opts.MapFrom(e => e.AlbumsSongs.Select(albumSong => albumSong.Song).OrderBy(s => s.TrackNumber)));
        }
    }
}
