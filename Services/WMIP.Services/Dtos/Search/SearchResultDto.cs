using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data.Models;

namespace WMIP.Services.Dtos.Search
{
    public class SearchResultDto : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public SearchResultType SearchResultType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, SearchResultDto>()
                .ForMember(m => m.Title, opts => opts.MapFrom(e => e.Name))
                .ForMember(m => m.SearchResultType, opts => opts.MapFrom(e => SearchResultType.Album));

            configuration.CreateMap<Song, SearchResultDto>()
                .ForMember(m => m.Title, opts => opts.MapFrom(e => e.Name))
                .ForMember(m => m.SearchResultType, opts => opts.MapFrom(e => SearchResultType.Song));

            configuration.CreateMap<Article, SearchResultDto>()
                .ForMember(m => m.Title, opts => opts.MapFrom(e => e.Title))
                .ForMember(m => m.SearchResultType, opts => opts.MapFrom(e => SearchResultType.Article));

            configuration.CreateMap<Review, SearchResultDto>()
                .ForMember(m => m.Title, opts => opts.MapFrom(e => e.Title))
                .ForMember(m => m.SearchResultType, opts => opts.MapFrom(e => SearchResultType.Review));
        }
    }
}
