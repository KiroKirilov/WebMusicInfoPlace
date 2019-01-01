using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Rating;

namespace WMIP.Web.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingsService ratingsService;
        private readonly IUsersService usersService;

        public RatingsController(IRatingsService ratingsService, IUsersService usersService)
        {
            this.ratingsService = ratingsService;
            this.usersService = usersService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Rate([FromBody]RatingViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);

            var newScore = this.ratingsService.Rate(model.PostId, userId, model.RatingType);

            return this.Json(new { newScore });
        }
    }
}
