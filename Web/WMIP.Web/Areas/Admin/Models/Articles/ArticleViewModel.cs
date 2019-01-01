﻿using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Dtos.Articles;

namespace WMIP.Web.Areas.Admin.Models.Articles
{
    public class ArticleViewModel : IMapTo<CreateDto>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: LengthConstants.TitleMaxLength, 
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
            MinimumLength = LengthConstants.MinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: LengthConstants.BodyMaxLength,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = LengthConstants.MinLength)]
        public string Body { get; set; }
        
        public string Summary { get; set; }
    }
}
