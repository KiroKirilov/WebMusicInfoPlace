using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Articles;
using WMIP.Web.Areas.Admin.Models.Articles;

namespace WMIP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesSerivce;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public ArticlesController(IArticlesService articlesSerivce, IUsersService usersService, IMapper mapper)
        {
            this.articlesSerivce = articlesSerivce;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ArticleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.View(model);
            }
            
            var creationInfo = this.mapper.Map<CreateDto>(model);
            creationInfo.UserId = userId;

            var creationResult = this.articlesSerivce.Create(creationInfo);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "created article");
                return this.RedirectToAction("Management", "Articles");
            }
            
            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "create article");
            return this.View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Management", "Articles");
            }

            var article = this.articlesSerivce.GetById(id.Value);
            if (article == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Management", "Articles");
            }
            var model = this.mapper.Map<ArticleViewModel>(article);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(ArticleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var editInfo = this.mapper.Map<EditPostDto>(model);

            var editResult = this.articlesSerivce.Edit(editInfo);

            if (editResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "edited article");
                return this.RedirectToAction("Management", "Articles");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit article");
            return this.View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Management", "Articles");
            }

            var article = this.articlesSerivce.GetById(id.Value);
            if (article == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the article you were looking for");
                return this.RedirectToAction("Management", "Articles");
            }
            var model = this.mapper.Map<ArticleViewModel>(article);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete(ArticleViewModel model)
        {
            var deletionResult = this.articlesSerivce.Delete(model.Id);

            if (deletionResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "deleted article");
                return this.RedirectToAction("Management", "Articles");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete article");
            return this.RedirectToAction("Management", "Articles");
        }

        public IActionResult Management()
        {
            var articles = this.articlesSerivce.GetAll().ToList();
            var mappedArticles = this.mapper.Map<IEnumerable<ArticleDisplayViewModel>>(articles);
            return this.View(mappedArticles);
        }
    }
}
