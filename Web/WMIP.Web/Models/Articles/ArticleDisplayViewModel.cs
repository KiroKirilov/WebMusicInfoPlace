﻿using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Dtos.Posts;

namespace WMIP.Web.Models.Articles
{
    public class ArticleDisplayViewModel : IMapFrom<UserRatedPostDto>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public int Score { get; set; }

        public RatingType CurrentUserRating { get; set; }
    }
}
