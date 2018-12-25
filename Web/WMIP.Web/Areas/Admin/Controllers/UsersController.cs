﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Admin.Models.Users;

namespace WMIP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IUsersService usersService;
        private IMapper mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public IActionResult Management()
        {
            var users = this.usersService.GetAllUsersWithRoles().ToList();
            var model = new UserManagementViewModel
            {
                Users = this.mapper.Map<UserViewModel[]>(users),
                AllRoles = UserConstants.Roles
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChangeRole([FromBody]ChangeRoleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { ok = false, reason = GenericErrorMessages.InvalidDataProvided });
            }

            var user = this.usersService.GetById(model.UserId);
            if (user == null || !UserConstants.Roles.Contains(model.NewRole))
            {
                return this.Json(new { ok = false, reason = string.Format(GenericErrorMessages.NotFound, "User") });
            }
            var successfullyChangedRole = this.usersService.SetUserRole(user, model.NewRole).GetAwaiter().GetResult();
            if (!successfullyChangedRole)
            {
                return this.Json(new { ok = false, reason = string.Format(GenericErrorMessages.CouldntDoSomething, "set role") });
            }

            return this.Json(new { ok = true });
        }
    }
}
