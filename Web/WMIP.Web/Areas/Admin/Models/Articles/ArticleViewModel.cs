using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Dtos.Posts;

namespace WMIP.Web.Areas.Admin.Models.Articles
{
    public class ArticleViewModel : IMapTo<CreatePostDto>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: LengthConstants.TitleMaxLength, 
            ErrorMessage = GenericMessages.InputStringLengthMinAndMaxErrorMessage, 
            MinimumLength = LengthConstants.MinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: LengthConstants.BodyMaxLength,
            ErrorMessage = GenericMessages.InputStringLengthMinOnlyErrorMessage,
            MinimumLength = LengthConstants.MinLength)]
        public string Body { get; set; }
        
        public string Summary { get; set; }
    }
}
