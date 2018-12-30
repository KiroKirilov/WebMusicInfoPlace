using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Artist.Models.Albums;
using WMIP.Web.Areas.Artist.Models.Songs;

namespace WMIP.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    public class AlbumsController : Controller
    {
        private readonly ISongsService songsService;
        private readonly IUsersService usersService;
        private readonly IAlbumsService albumsService;
        private readonly IMapper mapper;

        public AlbumsController(ISongsService songsService, IUsersService usersService, IAlbumsService albumsService, IMapper mapper)
        {
            this.songsService = songsService;
            this.usersService = usersService;
            this.albumsService = albumsService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var model = new AlbumViewModel();
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            this.EnsureModelSongs(model, userId);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(AlbumViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                this.EnsureModelSongs(model);
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                this.EnsureModelSongs(model, userId);
                return this.View(model);
            }

            var creationResult = this.albumsService.CreateNew(
                model.Name, model.Genre, model.ReleaseDate, model.ReleaseStage, model.SpotifyLink, model.AlbumCoverLink, model.SelectedSongIds, userId);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "submitted album for approval");
                return this.RedirectToAction("My", "Albums");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit album for approval");
            this.EnsureModelSongs(model, userId);
            return this.View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("My", "Albums");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userCanEditItem = this.albumsService.IsUserCreator(userId, id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var album = this.albumsService.GetById(id.Value);

            if (album == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("My", "Albums");
            }
            var model = this.mapper.Map<AlbumViewModel>(album);
            this.EnsureModelSongs(model, userId, model.SelectedSongIds);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(AlbumViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                this.EnsureModelSongs(model);
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                this.EnsureModelSongs(model, userId);
                return this.View(model);
            }

            var userCanEditItem = this.albumsService.IsUserCreator(userId, model.Id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var editResult = this.albumsService.Edit(
                model.Id.Value, model.Name, model.Genre, model.ReleaseDate, model.ReleaseStage, model.SpotifyLink, model.AlbumCoverLink, model.SelectedSongIds);

            if (editResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "submitted album for approval");
                return this.RedirectToAction("MyRequests", "Approval");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit edit for approval");
            this.EnsureModelSongs(model, userId, model.SelectedSongIds);
            return this.View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("My", "Albums");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userIsCreator = this.albumsService.IsUserCreator(userId, id.Value);

            if (!userIsCreator)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var album = this.albumsService.GetById(id.Value);
            var model = this.mapper.Map<AlbumViewModel>(album);
            this.EnsureModelSongs(model, userId, model.SelectedSongIds);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete(AlbumViewModel model)
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                this.EnsureModelSongs(model);
                return this.RedirectToAction("My", "Albums");
            }

            var userIsCreator = this.albumsService.IsUserCreator(userId, model.Id.Value);

            if (!userIsCreator)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var deletetionResult = this.albumsService.Delete(model.Id.Value);

            if (deletetionResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "deleted album");
                return this.RedirectToAction("My", "Albums");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete album");
            return this.RedirectToAction("My", "Albums");
        }

        private void EnsureModelSongs(AlbumViewModel model, string userId = null, IEnumerable<int> selectedIds = null)
        {
            if (userId == null)
            {
                model.AvailableSongs = new SelectListItem[0];
                return;
            }

            if (selectedIds == null)
            {
                selectedIds = new int[0];
            }

            try
            {
                var songs = this.songsService.GetUsersApprovedSongs(userId)
                    .Select(s => new SelectListItem(s.Name, s.Id.ToString(), selectedIds.Contains(s.Id)));
                model.AvailableSongs = songs;
            }
            catch
            {
                model.AvailableSongs = new SelectListItem[0];
            }
        }

        public IActionResult My()
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find artist");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            var albumsByUser = this.albumsService.GetAllAlbumsByUser(userId);
            var mappedSongs = this.mapper.Map<IEnumerable<MyAlbumViewModel>>(albumsByUser);

            return this.View(mappedSongs);
        }
    }
}
