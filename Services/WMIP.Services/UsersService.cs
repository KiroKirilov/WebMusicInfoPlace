using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class UsersService : IUsersService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly WmipDbContext context;

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager, WmipDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.context.Users;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            var loginResult = this.signInManager.PasswordSignInAsync(username, password, rememberMe, false).Result;

            return loginResult.Succeeded;
        }

        public void Logout()
        {
            this.signInManager.SignOutAsync().Wait();
        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName, string lastName)
        {
            if (password != confirmPassword)
            {
                return false;
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };

            var creationResult = this.userManager.CreateAsync(user, password).Result;

            if (creationResult.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                this.signInManager.SignInAsync(user, false).Wait();
            }

            return creationResult.Succeeded;
        }
    }
}
