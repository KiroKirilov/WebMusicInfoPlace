using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Songs;
using WMIP.Web.Areas.Artist.Models.Songs;

namespace WMIP.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    public class SongsController : Controller
    {
        private readonly ISongsService songsService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public SongsController(ISongsService songsService, IUsersService usersService, IMapper mapper)
        {
            this.songsService = songsService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View(new SongViewModel());
        }

        [HttpPost]
        public IActionResult Create(SongViewModel model)
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

            var creationInfo = this.mapper.Map<CreateSongDto>(model);
            creationInfo.ArtistId = userId;

            var creationResult = this.songsService.Create(creationInfo);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "submitted song for approval");
                return this.RedirectToAction("MyRequests", "Approval");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit song for approval");
            return this.View(model);
        }

        public IActionResult My()
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var songsByUser = this.songsService.GetAllSongsByUser(userId);
            var mappedSongs = this.mapper.Map<IEnumerable<MySongViewModel>>(songsByUser);

            return this.View(mappedSongs);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the song you were looking for");
                return this.RedirectToAction("My", "Songs");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userCanEditItem = this.songsService.IsUserCreatorById(userId, id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var song = this.songsService.GetById(id.Value);
            if (song == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the song you were looking for");
                return this.RedirectToAction("My", "Songs");
            }
            var model = this.mapper.Map<SongViewModel>(song);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(SongViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userCanEditItem = this.songsService.IsUserCreatorById(userId, model.Id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var editInfo = this.mapper.Map<EditSongDto>(model);

            var editResult = this.songsService.Edit(editInfo);

            if (editResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "submitted song for approval");
                return this.RedirectToAction("MyRequests", "Approval");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit edit for approval");
            return this.View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the song you were looking for");
                return this.RedirectToAction("My", "Songs");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userIsCreator = this.songsService.IsUserCreatorById(userId, id.Value);

            if (!userIsCreator)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var song = this.songsService.GetById(id.Value);
            if (song == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the song you were looking for");
                return this.RedirectToAction("My", "Songs");
            }
            var model = this.mapper.Map<SongViewModel>(song);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete(SongViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.View(model);
            }

            var userIsCreator = this.songsService.IsUserCreatorById(userId, model.Id.Value);

            if (!userIsCreator)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "load item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var deletionResult = this.songsService.Delete(model.Id.Value);

            if (deletionResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "deleted song");
                return this.RedirectToAction("My", "Songs");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete song");
            return this.RedirectToAction("My", "Songs");
        }
    }
}
