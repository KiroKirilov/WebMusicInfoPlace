using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class UsersServiceTests : BaseTestClass
    {
        [Theory]
        [InlineData("Ivan")]
        [InlineData("Pesho")]
        [InlineData("4d77f052-ae2c-45bc-8c79-74c38d6a2193")]
        public void GetByUsername_GetsTheUserWithTheGivenUsername(string id)
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var usersService = new UsersService(signInManager, userManager, context);
            context.Add(new User() { Id = id });
            context.SaveChanges();

            // Act
            var user = usersService.GetById(id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        public void Register_AddsUserToTheDatabase()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var rolesManager = this.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roleCreationResult = rolesManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            var usersService = new UsersService(signInManager, userManager, context);

            // Act
            usersService.Register("Uname", "Pword", "mail@mail.mail").GetAwaiter().GetResult();

            // Assert
            Assert.Equal(1, context.Users.Count());
        }

        [Fact]
        public void ChangeUserRole_ChangesTheUsersRole()
        {
            //Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var rolesManager = this.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var adminCreationResult = rolesManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            var userCreationResult = rolesManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            var usersService = new UsersService(signInManager, userManager, context);
            usersService.Register("Uname", "Pword", "mail@mail.mail").GetAwaiter().GetResult();
            var user = usersService.GetByUsername("Uname");

            // Act
            usersService.SetUserRole(user, "User").GetAwaiter().GetResult();
            var newRole = context.Roles.FirstOrDefault(r => r.Id == user.Roles.FirstOrDefault().RoleId)?.Name;

            // Assert
            Assert.Equal("User", newRole);
        }

        [Theory]
        [InlineData("Ivan")]
        [InlineData("Pesho")]
        [InlineData("")]
        public void GetIdFromUsername_GetsTheCorrectId(string username)
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var user = new User()
            {
                UserName = username,
                Id = "1"
            };
            context.Users.Add(user);
            context.SaveChanges();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var rolesManager = this.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var usersService = new UsersService(signInManager, userManager, context);

            // Act
            var result = usersService.GetIdFromUsername(username);

            // Assert
            Assert.Equal(user.Id, result);
        }

        [Fact]
        public void GetRolesForUser_GetsTheUsersRole()
        {
            //Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var rolesManager = this.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var adminCreationResult = rolesManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            var usersService = new UsersService(signInManager, userManager, context);
            usersService.Register("Uname", "Pword", "mail@mail.mail").GetAwaiter().GetResult();
            var user = usersService.GetByUsername("Uname");

            // Act
            var roles = usersService.GetRolesForUser(user);

            // Assert
            Assert.True(roles.First() == "Admin");
        }

        [Fact]
        public void GetAllUsersWithRoles_GetsUserAndMapsRoles()
        {
            //Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var rolesManager = this.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var adminCreationResult = rolesManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            var usersService = new UsersService(signInManager, userManager, context);
            usersService.Register("Uname", "Pword", "mail@mail.mail").GetAwaiter().GetResult();
            var user = usersService.GetByUsername("Uname");

            // Act
            var users = usersService.GetAllUsersWithRoles();

            // Assert
            Assert.Single(users);
            Assert.Equal(user.UserName, users.First().UserName);
            Assert.True(users.First().Roles.First() == "Admin");
        }
    }
}
