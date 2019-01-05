﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Data.Models
{
    public class Song : BaseMusicModel
    {
        public Song()
            : base()
        { }
        
        public string MusicVideoLink { get; set; }

        public string Lyrics { get; set; }
        
        [Required]
        public int TrackNumber { get; set; }
    }
}
