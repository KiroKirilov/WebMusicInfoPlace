using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants.Enums;
using WMIP.Data.Models.Enums;

namespace WMIP.Web.Areas.Admin.Models.Approval
{
    public class BasicApprovalItemViewModel
    {
        [Required]
        public string ItemId { get; set; }

        [Required]
        public ActionItemType ItemType { get; set; }

        [Required]
        public ApprovalStatus NewApprovalStatus { get; set; }
    }
}
