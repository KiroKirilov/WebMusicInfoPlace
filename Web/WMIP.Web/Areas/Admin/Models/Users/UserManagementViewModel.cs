using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMIP.Web.Areas.Admin.Models.Users
{
    public class UserManagementViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<string> AllRoles { get; set; }
    }
}
