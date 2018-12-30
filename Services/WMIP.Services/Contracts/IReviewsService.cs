using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Contracts
{
    public interface IReviewsService
    {
        bool CreateNew(string title, string body, string summary, int reviewScore, int albumId, string userId, ReviewType reviewType);

        bool Edit(int reviewId, string title, string body, string summary, int reviewScore, ReviewType reviewType);

        bool Delete(int reviewId);

        ReviewType GetReviewType(IEnumerable<string> userRoles);

        Review GetById(int reviewId);

        IEnumerable<Review> GetReviewsByUser(string userId);

        bool IsUserCreator(string userId, int reviewId);
    }
}
