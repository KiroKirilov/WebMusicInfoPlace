﻿using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Data.Models
{
    public class Review : Post
    {
        public Review()
            : base()
        { }

        public string Summary { get; set; }

        public int ReviewScore { get; set; }

        public ReviewType ReviewType { get; set; }

        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
