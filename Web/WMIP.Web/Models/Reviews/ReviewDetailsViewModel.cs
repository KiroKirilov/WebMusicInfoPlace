using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Web.Models.Comments;

namespace WMIP.Web.Models.Reviews
{
    public class ReviewDetailsViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int ReviewScore { get; set; }

        public DateTime CreatedOn { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public ReviewType ReviewType { get; set; }

        public string ReviewerName { get; set; }

        public int Score { get; set; }

        public IEnumerable<CommentDisplayViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ReviewDetailsViewModel>()
                .ForMember(m => m.AlbumName, opts => opts.MapFrom(e => e.Album.Name))
                .ForMember(m => m.ReviewerName, opts => opts.MapFrom(e => e.User.UserName))
                .ForMember(m => m.Comments, opts => opts.MapFrom(e => e.Comments.OrderByDescending(c => c.CreatedOn)))
                .ForMember(m => m.Score, opts => opts.MapFrom(e => e.Ratings.Sum(r => (int)r.RatingType)));
        }
    }
}
