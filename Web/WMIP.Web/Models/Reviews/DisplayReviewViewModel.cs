using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Reviews
{
    public class DisplayReviewViewModel : IHaveCustomMappings
    {
        private const int SummaryLength = 100;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Reviewer { get; set; }

        public int ReviewScore { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, DisplayReviewViewModel>()
                .ForMember(m => m.Reviewer, opts => opts.MapFrom(e => e.User.UserName));
        }
    }
}
