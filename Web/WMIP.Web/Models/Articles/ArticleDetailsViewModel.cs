using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Web.Models.Comments;

namespace WMIP.Web.Models.Articles
{
    public class ArticleDetailsViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentDisplayViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(m => m.AuthorName, opts => opts.MapFrom(e => e.User.UserName))
                .ForMember(m => m.Comments, opts => opts.MapFrom(e => e.Comments.OrderByDescending(c => c.CreatedOn)));
        }
    }
}
