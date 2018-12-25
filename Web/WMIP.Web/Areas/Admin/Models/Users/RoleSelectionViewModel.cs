using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMIP.Web.Areas.Admin.Models.Users
{
    public class RoleSelectionViewModel
    {
        public IEnumerable<string> Roles { get; set; }

        public string SelectedRole { get; set; }

        public string UserId { get; set; }
    }
}
