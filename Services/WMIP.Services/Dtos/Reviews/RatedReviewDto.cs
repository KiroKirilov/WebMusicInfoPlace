using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Posts;

namespace WMIP.Services.Dtos.Reviews
{
    public class RatedReviewDto : UserRatedPostDto
    {
        public int ReviewScore { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public ReviewType ReviewType { get; set; }
    }
}
