using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Albums;

namespace WMIP.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;
        private readonly IMapper mapper;

        public AlbumsController(IAlbumsService albumsService, IMapper mapper)
        {
            this.albumsService = albumsService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find album");
                return this.RedirectToAction("Index", "Home");
            }

            var album = this.albumsService.GetById(id.Value);
            var model = this.mapper.Map<AlbumDetailsViewModel>(album);

            return this.View(model);
        }
    }
}
