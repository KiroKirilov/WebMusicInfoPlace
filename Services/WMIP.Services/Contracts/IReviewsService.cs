using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Reviews;

namespace WMIP.Services.Contracts
{
    public interface IReviewsService : ICrudableEntityService<CreateReviewDto, EditReviewDto, Review, int>
    {
        ReviewType GetReviewType(IEnumerable<string> userRoles);

        IEnumerable<Review> GetReviewsByUser(string userId);

        bool IsUserCreator(string userId, int reviewId);
    }
}
