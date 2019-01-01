using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Songs
{
    public class SongDetailsCreatorViewModel : SongDetailsViewModel
    {
        public override void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Song, SongDetailsCreatorViewModel>()
                   .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName))
                   .ForMember(m => m.Albums, opts => opts.MapFrom(e => e.AlbumsSongs.Select(a => a.Album)));
        }
    }
}
