using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Areas.Admin.Models.Approval
{
    public class ApprovalItemViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Requester { get; set; }

        public string Genre { get; set; }

        public string ReleaseDate { get; set; }

        public ReleaseStage ReleaseStage { get; set; }

        public string ItemType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BaseMusicModel, ApprovalItemViewModel>()
                .ForMember(x => x.Requester, opts => opts.MapFrom(e => e.Artist.UserName))
                .ForMember(x => x.ReleaseDate, opts => opts.MapFrom(e => e.ReleaseDate.HasValue ? e.ReleaseDate.Value.ToLongDateString() : "None Specified."))
                .ForMember(x => x.ItemType, opts => opts.MapFrom(e => e.GetType().Name.Replace("Proxy", string.Empty)));
        }
    }
}
