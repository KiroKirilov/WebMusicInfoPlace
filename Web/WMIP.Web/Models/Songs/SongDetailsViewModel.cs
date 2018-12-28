using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Web.Models.Albums;

namespace WMIP.Web.Models.Songs
{
    public class SongDetailsViewModel : IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public string ArtistName { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string MusicVideoLink { get; set; }

        public string Lyrics { get; set; }

        public IEnumerable<AlbumBasicInfoViewModel> Albums { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Song, SongDetailsViewModel>()
                .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName))
                .ForMember(m => m.Albums, opts => opts.MapFrom(e => e.AlbumsSongs.Select(a => a.Album)));
        }
    }
}
