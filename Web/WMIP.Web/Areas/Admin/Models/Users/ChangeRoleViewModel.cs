using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMIP.Web.Areas.Admin.Models.Users
{
    public class ChangeRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string NewRole { get; set; }
    }
}
