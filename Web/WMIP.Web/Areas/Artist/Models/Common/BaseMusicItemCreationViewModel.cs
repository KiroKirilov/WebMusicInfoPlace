using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Areas.Artist.Models.Common
{
    public abstract class BaseMusicItemCreationViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
        [Required]
        [Display(Name = "Release Stage")]
        public ReleaseStage ReleaseStage { get; set; }
    }
}
