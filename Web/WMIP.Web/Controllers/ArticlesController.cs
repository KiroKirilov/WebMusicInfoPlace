using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Articles;

namespace WMIP.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly IRatingsService ratingsService;
        private readonly IMapper mapper;

        public ArticlesController(IArticlesService articlesService, IRatingsService ratingsService, IMapper mapper)
        {
            this.articlesService = articlesService;
            this.ratingsService = ratingsService;
            this.mapper = mapper;
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var article = this.articlesService.GetById(id.Value);

            if (article == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<ArticleDetailsViewModel>(article);

            return this.View(model);
        }

        public IActionResult All()
        {
            var articles = this.articlesService.GetAll().OrderByDescending(a => a.CreatedOn).ToList();
            var mappedArticles = this.mapper.Map<IEnumerable<ArticleDisplayViewModel>>(articles);

            return this.View(mappedArticles);
        }
    }
}
