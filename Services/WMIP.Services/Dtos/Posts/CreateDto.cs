using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Dtos.Articles
{
    public class CreateDto : PostCrudDto
    {
        public string UserId { get; set; }
    }
}
