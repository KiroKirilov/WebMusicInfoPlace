using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Admin.Models.Articles;

namespace WMIP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesSerivce;
        private readonly IUsersService usersService;

        public ArticlesController(IArticlesService articlesSerivce, IUsersService usersService)
        {
            this.articlesSerivce = articlesSerivce;
            this.usersService = usersService;
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

            var creationResult = this.articlesSerivce.CreateNew(model.Title, model.Body, userId);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "created article");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            
            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "create article");
            return this.View(model);
        }
    }
}
