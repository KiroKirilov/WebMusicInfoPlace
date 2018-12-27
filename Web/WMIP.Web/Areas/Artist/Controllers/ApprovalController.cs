using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Artist.Models.Approval;

namespace WMIP.Web.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    public class ApprovalController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IApprovalService approvalService;
        private readonly IMapper mapper;

        public ApprovalController(IUsersService usersService, IApprovalService approvalService, IMapper mapper)
        {
            this.usersService = usersService;
            this.approvalService = approvalService;
            this.mapper = mapper;
        }

        public IActionResult MyRequests()
        {
            var userId = this.usersService.GetIdFromUsername(this.User.Identity.Name);
            if (userId == null)
            {
                this.TempData["Error"] = string.Format(GenericMessages.CouldntDoSomething, "find user");
                return this.Redirect(Url.Action("Index", "Home", new { area = "" }));
            }

            try
            {
                var usersRequests = this.approvalService.GetUsersApprovalRequests(userId);
                var mappedItems = this.mapper.Map<MyApprovalRequestViewModel[]>(usersRequests);
                return this.View(mappedItems);
            }
            catch
            {
                return this.View(new MyApprovalRequestViewModel[0]);
            }
        }
    }
}
