using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data.Models;

namespace WMIP.Services.Dtos.Search
{
    public class SearchResultDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public SearchResultType SearchResultType { get; set; }
    }
}
