using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Reviews;

namespace WMIP.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IAlbumsService albumsService;
        private readonly IUsersService usersService;
        private readonly IReviewsService reviewsService;
        private readonly IMapper mapper;

        public ReviewsController(IAlbumsService albumsService, IUsersService usersService, IReviewsService reviewsService, IMapper mapper)
        {
            this.albumsService = albumsService;
            this.usersService = usersService;
            this.reviewsService = reviewsService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var album = this.albumsService.GetById(id.Value);
            if (album == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var model = new CreateReviewViewModel
            {
                AlbumId = album.Id,
                AlbumName = album.Name
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateReviewViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.usersService.GetByUsername(this.User.Identity.Name);
            if (user == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.View(model);
            }

            var albumExists = this.albumsService.DoesAlbumExist(model.AlbumId.Value, model.AlbumName);

            if (!albumExists)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.View(model);
            }

            var userRoles = this.usersService.GetRolesForUser(user);
            var reviewType = this.reviewsService.GetReviewType(userRoles);
            var creationResult = this.reviewsService.CreateNew(model.Title, model.Body, model.Summary, model.ReviewScore.Value, model.AlbumId.Value, user.Id, reviewType);

            if (creationResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "created review");
                return this.RedirectToAction("Details", "Albums", new { id = model.AlbumId.Value });
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "create a review");
            return this.View(model);
        }

        public IActionResult All(int? id, string reviewType)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var album = this.albumsService.GetById(id.Value);
            if (album == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the album you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            bool reviewTypeIsValid = Enum.TryParse(reviewType, true, out ReviewType parsedReviewType);
            IEnumerable<Review> relevantReviews = new List<Review>();
            string selectedFilter = string.Empty;
            if (reviewTypeIsValid)
            {
                relevantReviews = album.Reviews.Where(r => r.ReviewType == parsedReviewType);
                selectedFilter = parsedReviewType.ToString();
            }
            else
            {
                relevantReviews = album.Reviews;
                selectedFilter = "All";
            }

            var mappedReviews = this.mapper.Map<DisplayReviewViewModel[]>(relevantReviews);

            var model = new AllReviewsViewModel()
            {
                AlbumId = id.Value,
                AlbumName = album.Name,
                SelectedFilter = selectedFilter,
                Reviews = mappedReviews
            };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var review = this.reviewsService.GetById(id.Value);
            if (review == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<ReviewDetailsViewModel>(review);

            return this.View(model);
        }
    }
}
