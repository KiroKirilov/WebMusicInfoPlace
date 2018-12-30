using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Data.Models.Common
{
    public abstract class Post : BaseModel<int>
    {
        public Post()
            : base()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Discriminator { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
