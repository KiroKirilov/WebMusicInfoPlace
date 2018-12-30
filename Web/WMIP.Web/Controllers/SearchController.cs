using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Search;

namespace WMIP.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IMapper mapper;

        public SearchController(ISearchService searchService, IMapper mapper)
        {
            this.searchService = searchService;
            this.mapper = mapper;
        }

        public IActionResult Find([FromQuery]string searchTerm)
        {
            var results = this.searchService.GetResults(searchTerm);

            return this.PartialView("SearchResultsPartial", results);
        }
    }
}
