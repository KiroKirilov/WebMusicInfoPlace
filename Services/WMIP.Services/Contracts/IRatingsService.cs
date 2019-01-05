using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Contracts
{
    public interface IRatingsService
    {
        int Rate(int postId, string userId, RatingType ratingType);

        RatingType GetUsersRatingTypeForAPost(int postId, string username);

        IEnumerable<Rating> GetUsersRatings(string username);
    }
}
