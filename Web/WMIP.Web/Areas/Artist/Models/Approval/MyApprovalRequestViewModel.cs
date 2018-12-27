using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Areas.Artist.Models.Approval
{
    public class MyApprovalRequestViewModel : IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public string ReleaseDate { get; set; }

        public ReleaseStage ReleaseStage { get; set; }

        public string ItemType { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BaseMusicModel, MyApprovalRequestViewModel>()
                .ForMember(x => x.ReleaseDate, opts => opts.MapFrom(e => e.ReleaseDate.HasValue ? e.ReleaseDate.Value.ToLongDateString() : "None Specified."))
                .ForMember(x => x.ItemType, opts => opts.MapFrom(e => e.GetType().Name.Replace("Proxy", string.Empty)));
        }
    }
}
