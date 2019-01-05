using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Users;

namespace WMIP.Services.Contracts
{
    public interface IUsersService : IGettableEntityService<User, string>
    {
        Task<bool> Register(string username, string password, string email);

        bool Login(string username, string password, bool rememberMe);

        void Logout();

        IEnumerable<UserDto> GetAllUsersWithRoles();

        User GetByUsername(string username);

        Task<bool> SetUserRole(User user, string role);

        string GetIdFromUsername(string username);

        IEnumerable<string> GetRolesForUser(User user);
    }
}
