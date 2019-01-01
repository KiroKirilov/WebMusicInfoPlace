using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Extensions;
using WMIP.Web.Areas.Artist.Models.Common;

namespace WMIP.Web.Areas.Artist.Models.Albums
{
    public class MyAlbumViewModel : BaseMusicItemViewModel, IHaveCustomMappings
    {
        public ApprovalStatus ApprovalStatus { get; set; }

        public double UserScore { get; set; }

        public double CriticScore { get; set; }

        public string UserClass => this.UserScore.ToButtonClass();

        public string CriticClass => this.CriticScore.ToButtonClass();

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, MyAlbumViewModel>()
                .ForMember(m => m.UserScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.User).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.User).Average(r => r.ReviewScore) : 0))
                .ForMember(m => m.CriticScore, opts => opts.MapFrom(e =>
                    e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Count() > 0 ? e.Reviews.Where(r => r.ReviewType == ReviewType.Critic).Average(r => r.ReviewScore) : 0));
        }
    }
}
