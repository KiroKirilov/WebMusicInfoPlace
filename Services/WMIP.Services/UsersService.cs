using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos;

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

        public IEnumerable<UserDto> GetAllUsersWithRoles()
        {
            return this.context.Users.Select(u => new UserDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName,
                Roles = u.Roles.SelectMany(userRole => this.context.Roles.Where(r => r.Id == userRole.RoleId).Select(r => r.Name)),
            });
        }

        public IEnumerable<string> GetRolesForUser(User user)
        {
            try
            {
                var roles = user.Roles
                    .SelectMany(userRole =>
                        this.context.Roles.Where(r => r.Id == userRole.RoleId)
                    .Select(r => r.Name));

                return roles;
            }
            catch
            {
                return new List<string>();
            }
        }

        public User GetById(string id)
        {
            var user = this.context.Users.Find(id);
            return user;
        }

        public User GetByUsername(string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public string GetIdFromUsername(string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                return user.Id;
            }

            return null;
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

        public async Task<bool> SetUserRole(User user, string newRole)
        {
            try
            {
                var previousRoles = await this.userManager.GetRolesAsync(user);
                if (previousRoles.Contains(newRole))
                {
                    return false;
                }
                await this.userManager.RemoveFromRolesAsync(user, previousRoles);
                var assignmentResult = await this.userManager.AddToRoleAsync(user, newRole);
                return assignmentResult.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
