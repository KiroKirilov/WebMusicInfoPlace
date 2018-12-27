using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Artist.Models.Songs;

namespace WMIP.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    public class SongsController : Controller
    {
        private readonly ISongsService songsService;
        private readonly IUsersService usersService;

        public SongsController(ISongsService songsService, IUsersService usersService)
        {
            this.songsService = songsService;
            this.usersService = usersService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateSongViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.View(model);
            }

            var creationResult = this.songsService.CreateNew(
                model.Name, model.Genre, model.ReleaseDate, model.ReleaseStage, model.TrackNumber, model.MusicVideoLink, model.Lyrics, userId);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "submitted song for approval");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit song for approval");
            return this.View(model);
        }
    }
}
