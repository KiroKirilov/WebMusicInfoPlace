using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly WmipDbContext context;

        public ReviewsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool CreateNew(string title, string body, string summary, int reviewScore, int albumId, string userId, ReviewType reviewType)
        {
            try
            {
                var review = new Review
                {
                    Title = title,
                    Body = body,
                    Summary = summary,
                    ReviewScore = reviewScore,
                    ReviewType = reviewType,
                    AlbumId = albumId,
                    UserId = userId,
                };

                this.context.Reviews.Add(review);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Review GetById(int reviewId)
        {
            return this.context.Reviews.Find(reviewId);
        }

        public ReviewType GetReviewType(IEnumerable<string> userRoles)
        {
            if (userRoles != null && userRoles.Contains("Critic"))
            {
                return ReviewType.Critic;
            }

            return ReviewType.User;
        }
    }
}
