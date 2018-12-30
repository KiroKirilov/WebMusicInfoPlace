using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Albums
{
    public class DatedAlbumViewModel : AlbumBasicInfoViewModel, IHaveCustomMappings
    {
        public DateTime? ReleaseDate { get; set; }

        public string ArtistName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, DatedAlbumViewModel>()
                .ForMember(m => m.ArtistName, opts => opts.MapFrom(e => e.Artist.UserName));
        }
    }
}
