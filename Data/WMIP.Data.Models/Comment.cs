using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models.Common;

namespace WMIP.Data.Models
{
    public class Comment : Post<int>
    {
        public Comment()
            : base()
        { }

        public int CommentedOnId { get; set; }
        public virtual Post<int> CommentedOn { get; set; }
    }
}
