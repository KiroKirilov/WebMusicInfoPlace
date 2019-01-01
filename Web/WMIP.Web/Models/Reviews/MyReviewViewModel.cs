using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Reviews
{
    public class MyReviewViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ReviewScore { get; set; }

        public string AlbumName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Score { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, MyReviewViewModel>()
                .ForMember(m => m.AlbumName, opts => opts.MapFrom(e => e.Album.Name))
                .ForMember(m => m.Score, opts => opts.MapFrom(e => e.Ratings.Sum(r => (int)r.RatingType)));
        }
    }
}
