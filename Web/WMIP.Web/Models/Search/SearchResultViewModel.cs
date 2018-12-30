using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Services.Dtos.Search;

namespace WMIP.Web.Models.Search
{
    public class SearchResultViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SearchResultType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SearchResultDto, SearchResultViewModel>()
                .ForMember(m => m.SearchResultType, opts => opts.MapFrom(e => e.SearchResultType.ToString()));
        }
    }
}
