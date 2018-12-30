using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class Comment : Post
    {
        public Comment()
            : base()
        { }

        public int? CommentedOnId { get; set; }
        public virtual Post CommentedOn { get; set; }
    }
}
