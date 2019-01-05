using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Posts;

namespace WMIP.Web.Models.Comments
{
    public class CommentDisplayViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public int Score { get; set; }

        public RatingType CurrentUserRating { get; set; }

        public IEnumerable<CommentDisplayViewModel> Replies { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UserRatedPostDto, CommentDisplayViewModel>()
                .ForMember(m => m.Replies, opts => opts.MapFrom(e => e.Comments.OrderByDescending(c => c.CreatedOn)));
        }
    }
}
