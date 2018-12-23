using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Data.Models
{
    public class Rating : BaseModel<int>
    {
        public Rating()
            : base()
        { }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public RatingType RatingType { get; set; }
    }
}
