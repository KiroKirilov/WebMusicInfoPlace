﻿using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;

namespace WMIP.Web.Models.Articles
{
    public class ArticleDisplayViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Article, ArticleDisplayViewModel>()
                .ForMember(m => m.AuthorName, opts => opts.MapFrom(e => e.User.UserName));
        }
    }
}
