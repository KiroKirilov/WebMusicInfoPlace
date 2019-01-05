using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string Summary { get; set; }

        public string Discriminator { get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
