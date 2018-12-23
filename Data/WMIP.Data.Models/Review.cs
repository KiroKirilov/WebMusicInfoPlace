using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class Review : Post<int>
    {
        public Review()
            : base()
        { }
        
        public int ReviewScore { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
