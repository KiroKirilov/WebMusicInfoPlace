using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Articles;

namespace WMIP.Services.Dtos.Reviews
{
    public class ReviewCrudDto : PostCrudDto
    {
        public ReviewType ReviewType { get; set; }

        public int ReviewScore { get; set; }
    }
}
