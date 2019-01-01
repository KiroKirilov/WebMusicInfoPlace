using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Reviews
{
    public class CreateReviewDto : ReviewCrudDto
    {
        public string UserId { get; set; }

        public int AlbumId { get; set; }
    }
}
