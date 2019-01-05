using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Comments
{
    public class CreateCommentDto
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }
    }
}
