using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Common.Comments
{
    public class CommentDisplayViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public string CurrentUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentDisplayViewModel> Replies { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentDisplayViewModel>()
                .ForMember(m => m.Author, opts => opts.MapFrom(e => e.User.UserName))
                .ForMember(m => m.Replies, opts => opts.MapFrom(e => e.Comments.OrderByDescending(c => c.CreatedOn)));
        }
    }
}
