using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Models.Rating
{
    public class MyRatingViewModel : IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public string PostType { get; set; }

        public RatingType RatingType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Data.Models.Rating, MyRatingViewModel>()
                .ForMember(m => m.PostType, opts => opts.MapFrom(e => e.Post.GetType().Name.Replace("Proxy", string.Empty)))
                .ForMember(m => m.PostTitle, opts => opts.MapFrom(e => e.Post.Title));
        }
    }
}
