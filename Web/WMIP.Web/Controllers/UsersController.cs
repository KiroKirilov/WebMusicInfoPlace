using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMIP.Data.Models;
using WMIP.Services.Contracts;
using WMIP.Web.Models.Users;

namespace WMIP.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UsersController(IUsersService userService, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isRegister = this.userService.Register(model.Username, model.Password, model.ConfirmPassword, model.Email,
                model.FirstName, model.LastName).GetAwaiter().GetResult();

            if (!isRegister)
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isLogin = this.userService.Login(model.Username, model.Password, model.RememberMe);

            if (!isLogin)
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            this.userService.Logout();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}
