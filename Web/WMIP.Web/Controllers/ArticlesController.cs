using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Web.Models.Articles;

namespace WMIP.Web.Controllers
{
    public class ArticlesController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ArticleViewModel model)
        {
            return this.View();
        }
    }
}
