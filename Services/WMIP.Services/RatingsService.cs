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
    public class RatingsService : IRatingsService
    {
        private readonly WmipDbContext context;

        public RatingsService(WmipDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Rating> GetUsersRatings(string username)
        {
            try
            {
                return this.context.Ratings.Where(r => r.User.UserName == username).ToList();
            }
            catch
            {
                return new List<Rating>();
            }
        }

        public RatingType GetUsersRatingTypeForAPost(int postId, string username)
        {
            var rating = this.context.Ratings.FirstOrDefault(r => r.User.UserName == username && r.PostId == postId);

            if (rating == null)
            {
                return RatingType.Neutral;
            }

            return rating.RatingType;
        }

        public int Rate(int postId, string userId, RatingType ratingType)
        {
            try
            {
                var rating = this.context.Ratings.FirstOrDefault(r => r.PostId == postId && r.UserId == userId);
                if (rating == null)
                {
                    rating = new Rating()
                    {
                        PostId = postId,
                        UserId = userId,
                        RatingType = ratingType
                    };
                    this.context.Ratings.Add(rating);
                }
                else if (rating.RatingType == ratingType)
                {
                    rating.RatingType = RatingType.Neutral;
                    this.context.Ratings.Update(rating);
                }
                else
                {
                    rating.RatingType = ratingType;
                    this.context.Ratings.Update(rating);
                }

                this.context.SaveChanges();

                var newScore = this.context.Ratings.Where(r => r.PostId == postId).Sum(r => (int)r.RatingType);

                return newScore;
            }
            catch
            {
                return 0;
            }
        }
    }
}
