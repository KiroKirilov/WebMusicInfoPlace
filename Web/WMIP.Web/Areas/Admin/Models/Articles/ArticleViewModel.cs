using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;

namespace WMIP.Web.Areas.Admin.Models.Articles
{
    public class ArticleViewModel
    {
        [Required]
        [StringLength(maximumLength: ArticleConstants.TitleMaxLength, 
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
            MinimumLength = ArticleConstants.MinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: ArticleConstants.BodyMaxLength,
            ErrorMessage = "The {0} must be at least {2} characters long.",
            MinimumLength = ArticleConstants.MinLength)]
        public string Body { get; set; }
    }
}
