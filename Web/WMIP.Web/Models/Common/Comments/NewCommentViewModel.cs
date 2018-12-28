using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMIP.Web.Models.Common.Comments
{
    public class NewCommentViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public int PostId { get; set; }

        [Required]
        public string Username { get; set; }

        public string Type { get; set; }
    }
}
