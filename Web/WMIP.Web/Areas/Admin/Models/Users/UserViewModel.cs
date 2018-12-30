using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Data.Models;
using WMIP.Services.Dtos.Users;

namespace WMIP.Web.Areas.Admin.Models.Users
{
    public class UserViewModel : IMapFrom<UserDto>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
