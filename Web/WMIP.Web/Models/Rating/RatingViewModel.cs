using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Models.Rating
{
    public class RatingViewModel
    {
        public int PostId { get; set; }

        public RatingType RatingType { get; set; }

        public int Score { get; set; }
    }
}
