using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Services.Dtos.Comments;

namespace WMIP.Web.Models.Comments
{
    public class NewCommentViewModel : IMapTo<CreateCommentDto>
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
