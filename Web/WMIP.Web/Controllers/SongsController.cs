using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Songs;

namespace WMIP.Web.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongsService songsService;
        private readonly IMapper mapper;

        public SongsController(ISongsService songsService, IMapper mapper)
        {
            this.songsService = songsService;
            this.mapper = mapper;
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find song");
                return this.RedirectToAction("Index", "Home");
            }

            var song = this.songsService.GetById(id.Value);
            if (song == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find song");
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<SongDetailsViewModel>(song);

            return this.View(model);
        }
    }
}
