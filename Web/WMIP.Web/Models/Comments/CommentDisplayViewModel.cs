using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Models.Comments
{
    public class CommentDisplayViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Score { get; set; }

        public RatingType UserRating { get; set; }

        public IEnumerable<CommentDisplayViewModel> Replies { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentDisplayViewModel>()
                .ForMember(m => m.Author, opts => opts.MapFrom(e => e.User.UserName))
                .ForMember(m => m.Replies, opts => opts.MapFrom(e => e.Comments.OrderByDescending(c => c.CreatedOn)))
                .ForMember(m => m.Score, opts => opts.MapFrom(e => e.Ratings.Sum(r => (int)r.RatingType)));
        }
    }
}
