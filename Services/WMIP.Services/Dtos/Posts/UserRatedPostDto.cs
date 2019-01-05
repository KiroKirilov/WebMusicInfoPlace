using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Dtos.Posts
{
    public class UserRatedPostDto : PostCrudDto
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public int Score { get; set; }

        public RatingType CurrentUserRating { get; set; }

        public IEnumerable<UserRatedPostDto> Comments { get; set; }
    }
}
