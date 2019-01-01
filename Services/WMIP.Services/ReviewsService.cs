using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Reviews;

namespace WMIP.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly WmipDbContext context;

        public ReviewsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool Create(CreateReviewDto creationInfo)
        {
            try
            {
                var review = new Review
                {
                    Title = creationInfo.Title,
                    Body = creationInfo.Body,
                    Summary = creationInfo.Summary,
                    ReviewScore = creationInfo.ReviewScore,
                    ReviewType = creationInfo.ReviewType,
                    AlbumId = creationInfo.AlbumId,
                    UserId = creationInfo.UserId,
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

        public bool Edit(EditReviewDto editInfo)
        {
            try
            {
                var review = this.context.Reviews.Find(editInfo.Id);
                if (review == null)
                {
                    return false;
                }
                review.Title = editInfo.Title;
                review.Body = editInfo.Body;
                review.Summary = editInfo.Summary;
                review.ReviewScore = editInfo.ReviewScore;
                review.ReviewType = editInfo.ReviewType;
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
            return this.context.Reviews.Any(r => r.UserId == userId && r.Id == reviewId);
        }
    }
}
