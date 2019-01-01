using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Models.Albums
{
    public class AlbumDetailsCreatorViewModel : AlbumDetailsViewModel
    {
        public override void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumDetailsCreatorViewModel>()
                .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName))
                .ForMember(m => m.UserScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.User).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.User).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.CriticScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.Songs, opts => opts.MapFrom(e => e.AlbumsSongs
                    .Select(albumSong => albumSong.Song)
                    .OrderBy(s => s.TrackNumber)));
        }
    }
}
