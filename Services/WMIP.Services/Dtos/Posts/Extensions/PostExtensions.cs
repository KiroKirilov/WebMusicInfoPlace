using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Dtos.Posts.Extensions
{
    public static class PostExtensions
    {
        public static UserRatedPostDto ToDto(this Post post, string username)
        {
            return new UserRatedPostDto
            {
                Id = post.Id,
                Summary = post.Summary,
                Body = post.Body,
                CreatedOn = post.CreatedOn,
                Title = post.Title,
                Comments = post.Comments?.ToList().OrderByDescending(p => p.CreatedOn).Select(p => p.ToDto(username)),
                AuthorName = post.User?.UserName,
                Score = post.Ratings.Sum(r => (int)r.RatingType),
                CurrentUserRating = post.Ratings?.FirstOrDefault(r => !string.IsNullOrWhiteSpace(username) && r.User?.UserName == username) == null ?
                        RatingType.Neutral :
                        post.Ratings.FirstOrDefault(r => !string.IsNullOrWhiteSpace(username) && r.User?.UserName == username).RatingType
            };
        }
    }
}
