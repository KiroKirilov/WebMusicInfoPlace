using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AlbumsController(ISongsService songsService, IUsersService usersService, IAlbumsService albumsService,IMapper mapper)
        {
            this.songsService = songsService;
            this.usersService = usersService;
            this.albumsService = albumsService;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var model = new CreateAlbumViewModel();
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
        public IActionResult Create(CreateAlbumViewModel model)
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
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "submit album for approval");
            this.EnsureModelSongs(model, userId);
            return this.View(model);
        }

        private void EnsureModelSongs(CreateAlbumViewModel model, string userId = null)
        {
            if (userId == null)
            {
                model.AvailableSongs = new SongSelectViewModel[0];
                return;
            }

            try
            {
                var songs = this.songsService.GetUsersApprovedSongs(userId);
                var mappedSongs = this.mapper.Map<SongSelectViewModel[]>(songs);
                model.AvailableSongs = mappedSongs;
            }
            catch
            {
                model.AvailableSongs = new SongSelectViewModel[0];
            }
        }
    }
}
