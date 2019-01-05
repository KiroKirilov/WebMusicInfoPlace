using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Posts.Extensions;
using WMIP.Services.Dtos.Reviews;

namespace WMIP.Services.Dtos.Posts.Reviews
{
    public static class ReviewExtensions
    {
        public static RatedReviewDto ToDto(this Review review, string username)
        {
            return new RatedReviewDto
            {
                Id = review.Id,
                Summary = review.Summary,
                Body = review.Body,
                CreatedOn = review.CreatedOn,
                Title = review.Title,
                Comments = review.Comments.ToList().Select(c => c.ToDto(username)),
                AuthorName = review.User.UserName,
                Score = review.Ratings.Sum(r => (int)r.RatingType),
                CurrentUserRating = review.Ratings.FirstOrDefault(r => r.User.UserName == username) == null ?
                        RatingType.Neutral :
                        review.Ratings.FirstOrDefault(r => r.User.UserName == username).RatingType,
                ReviewScore = review.ReviewScore,
                AlbumId = review.Album.Id,
                AlbumName = review.Album.Name,
                ReviewType = review.ReviewType
            };
        }
    }
}
