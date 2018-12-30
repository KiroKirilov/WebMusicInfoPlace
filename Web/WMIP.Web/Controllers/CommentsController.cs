using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Data.Models;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Comments;

namespace WMIP.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public CommentsController(ICommentsService commentsService, IUsersService usersService, IMapper mapper)
        {
            this.commentsService = commentsService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(NewCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { ok = false, reason = GenericMessages.InvalidDataProvided });
            }

            var userId = this.usersService.GetIdFromUsername(model.Username);

            var creationResult = this.commentsService.CreateNew(model.Title, model.Body, userId, model.PostId, out Comment comment);
            if (!creationResult)
            {
                return this.Json(new { ok = false, reason = string.Format(GenericMessages.InvalidDataProvided) });
            }

            var commentInfo = new {
                title = comment.Title,
                body = comment.Body,
                author = comment.User.UserName,
                date = comment.CreatedOn.ToShortDateString(),
                time = comment.CreatedOn.ToShortTimeString()
            };

            return this.Json(new { ok = true, info = commentInfo});
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

            var commentsByUser = this.commentsService.GetCommentsByUser(userId);
            var mappedComments= this.mapper.Map<IEnumerable<MyCommentViewModel>>(commentsByUser);

            return this.View(mappedComments);
        }
    }
}
