using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Posts
{
    public class CreatePostDto : PostCrudDto
    {
        public string UserId { get; set; }
    }
}
