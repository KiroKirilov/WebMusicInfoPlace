using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WMIP.Constants;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Web.Models;
using WMIP.Web.Models.Albums;
using WMIP.Web.Models.Articles;
using WMIP.Web.Models.Home;

namespace WMIP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly IAlbumsService albumsService;
        private readonly IMapper mapper;

        public HomeController(IArticlesService articlesService, IAlbumsService albumsService, IMapper mapper)
        {
            this.articlesService = articlesService;
            this.albumsService = albumsService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var latestArticles = this.articlesService.GetLatest(HomePageConstants.ArticlesToShow);
            var mappedLatestArticles = this.mapper.Map<IEnumerable<ArticleDisplayViewModel>>(latestArticles);

            var latestAlbums = this.albumsService.GetLatestReleases(HomePageConstants.AlbumsToShow);
            var mappedLatestAlbums = this.mapper.Map<IEnumerable<DatedAlbumViewModel>>(latestAlbums);

            var comingSoonAlbums = this.albumsService.GetComingSoonReleases(HomePageConstants.AlbumsToShow);
            var mappedComingSoonAlbums = this.mapper.Map<IEnumerable<DatedAlbumViewModel>>(comingSoonAlbums);

            var userAcclaimedAlbums = this.albumsService.GetMostAcclaimed(ReviewType.User, HomePageConstants.AlbumsToShow);
            var mappedUserAcclaimedAlbums = this.mapper.Map<IEnumerable<ScoredAlbumViewModel>>(userAcclaimedAlbums);

            var criticAcclaimedAlbums = this.albumsService.GetMostAcclaimed(ReviewType.Critic, HomePageConstants.AlbumsToShow);
            var mappedCriticAcclaimedAlbums = this.mapper.Map<IEnumerable<ScoredAlbumViewModel>>(criticAcclaimedAlbums);

            var model = new HomePageViewModel
            {
                LatestArticles = mappedLatestArticles,
                LatestReleases = mappedLatestAlbums,
                ComingSoon = mappedComingSoonAlbums,
                CriticsMostAcclaimed = mappedCriticAcclaimedAlbums,
                UsersMostAcclaimed = mappedUserAcclaimedAlbums
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
