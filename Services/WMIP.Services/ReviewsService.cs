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

        public bool Delete(int reviewId)
        {
            try
            {
                var review = this.context.Reviews.Find(reviewId);
                if (review==null)
                {
                    return false;
                }
                foreach (var comment in review.Comments)
                {
                    comment.CommentedOnId = null;
                    this.context.Comments.Update(comment);
                }
                this.context.Reviews.Remove(review);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(int reviewId, string title, string body, string summary, int reviewScore, ReviewType reviewType)
        {
            try
            {
                var review = this.context.Reviews.Find(reviewId);
                if (review == null)
                {
                    return false;
                }
                review.Title = title;
                review.Body = body;
                review.Summary = summary;
                review.ReviewScore = reviewScore;
                review.ReviewType = reviewType;
                this.context.Reviews.Update(review);
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

        public IEnumerable<Review> GetReviewsByUser(string userId)
        {
            try
            {
                return this.context.Reviews.Where(r => r.UserId == userId).ToList();
            }
            catch
            {
                return new List<Review>();
            }
        }

        public ReviewType GetReviewType(IEnumerable<string> userRoles)
        {
            if (userRoles != null && userRoles.Contains("Critic"))
            {
                return ReviewType.Critic;
            }

            return ReviewType.User;
        }

        public bool IsUserCreator(string userId, int reviewId)
        {
            var review = this.context.Reviews.FirstOrDefault(r => r.UserId == userId && r.Id == reviewId);

            return review != null;
        }
    }
}
