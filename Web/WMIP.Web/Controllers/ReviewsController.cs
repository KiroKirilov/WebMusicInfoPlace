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
using WMIP.Services.Dtos.Posts.Reviews;
using WMIP.Services.Dtos.Reviews;
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

            var model = new ReviewViewModel
            {
                AlbumId = album.Id,
                AlbumName = album.Name
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ReviewViewModel model)
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
            var creationInfo = this.mapper.Map<CreateReviewDto>(model);
            creationInfo.ReviewType = reviewType;
            creationInfo.UserId = user.Id;
            var creationResult = this.reviewsService.Create(creationInfo);

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
            IEnumerable<RatedReviewDto> relevantReviews = new List<RatedReviewDto>();
            string selectedFilter = string.Empty;
            if (reviewTypeIsValid)
            {
                relevantReviews = album.Reviews.Where(r => r.ReviewType == parsedReviewType).Select(r => r.ToDto(this.User.Identity.Name));
                selectedFilter = parsedReviewType.ToString();
            }
            else
            {
                relevantReviews = album.Reviews.Select(r => r.ToDto(this.User.Identity.Name));
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

            var model = this.mapper.Map<ReviewDetailsViewModel>(review.ToDto(this.User.Identity.Name));

            return this.View(model);
        }

        [Authorize]
        public IActionResult My()
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.RedirectToAction("Index", "Home");
            }

            var reviewsByUser = this.reviewsService.GetReviewsByUser(userId);
            var mappedReviews = this.mapper.Map<IEnumerable<MyReviewViewModel>>(reviewsByUser);

            return this.View(mappedReviews);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("My", "Reviews");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userCanEditItem = this.reviewsService.IsUserCreator(userId, id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var review = this.reviewsService.GetById(id.Value);
            if (review == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("My", "Reviews");
            }
            var model = this.mapper.Map<ReviewViewModel>(review);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(ReviewViewModel model)
        {
            var user = this.usersService.GetByUsername(this.User.Identity.Name);
            if (user == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userCanEditItem = this.reviewsService.IsUserCreator(user.Id, model.Id);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit item");
                return this.RedirectToAction("My", "Reviews");
            }

            var userRoles = this.usersService.GetRolesForUser(user);
            var reviewType = this.reviewsService.GetReviewType(userRoles);
            var editInfo = this.mapper.Map<EditReviewDto>(model);
            editInfo.ReviewType = reviewType;
            var editResult = this.reviewsService.Edit(editInfo);

            if (editResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "edited review");
                return this.RedirectToAction("My", "Reviews");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "edit review");
            return this.View(model);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("My", "Reviews");
            }

            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var userCanEditItem = this.reviewsService.IsUserCreator(userId, id.Value);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete item");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            var review = this.reviewsService.GetById(id.Value);
            if (review == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find the review you were looking for");
                return this.RedirectToAction("My", "Reviews");
            }
            var model = this.mapper.Map<ReviewViewModel>(review);
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(ReviewViewModel model)
        {
            var user = this.usersService.GetByUsername(this.User.Identity.Name);
            if (user == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.RedirectToAction("My", "Reviews");
            }

            var userCanEditItem = this.reviewsService.IsUserCreator(user.Id, model.Id);

            if (!userCanEditItem)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete item");
                return this.RedirectToAction("My", "Reviews");
            }

            var userRoles = this.usersService.GetRolesForUser(user);
            var reviewType = this.reviewsService.GetReviewType(userRoles);
            var deletionResult = this.reviewsService.Delete(model.Id);

            if (deletionResult)
            {
                this.TempData["Success"] = string.Format(GenericMessages.SuccessfullyDidSomething, "deleted review");
                return this.RedirectToAction("My", "Reviews");
            }

            this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "delete review");
            return this.RedirectToAction("My", "Reviews");
        }
    }
}
