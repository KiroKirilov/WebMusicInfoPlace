using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Dtos.Reviews;

namespace WMIP.Web.Models.Reviews
{
    public class ReviewViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(maximumLength: LengthConstants.SummaryMaxLength,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = LengthConstants.MinLength)]
        public string Summary { get; set; }

        [Required]
        [Range(ReviewConstants.MinScore, ReviewConstants.MaxScore, ErrorMessage = ReviewConstants.ScoreNotInRageErrorMessage)]
        [Display(Name = "Score")]
        public int? ReviewScore { get; set; }

        [Required]
        public int? AlbumId { get; set; }

        [Required]
        public string AlbumName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ReviewViewModel, CreateReviewDto>();
            configuration.CreateMap<ReviewViewModel, EditReviewDto>();
        }
    }
}
