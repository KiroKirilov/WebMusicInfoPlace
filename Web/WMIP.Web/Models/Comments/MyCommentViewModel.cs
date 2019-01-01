using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Common;

namespace WMIP.Web.Models.Comments
{
    public class MyCommentViewModel : IHaveCustomMappings
    {
        public string Title { get; set; }

        public string PostTitle { get; set; }

        public string PostType { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Score { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, MyCommentViewModel>()
                .ForMember(m => m.Score, opts => opts.MapFrom(e => e.Ratings.Sum(r => (int)r.RatingType)))
                .ForMember(m => m.PostTitle, opts => opts.MapFrom(e => e.Title))
                .AfterMap((src, dest) => dest.PostType = src.CommentedOn.Discriminator);
        }
    }
}
