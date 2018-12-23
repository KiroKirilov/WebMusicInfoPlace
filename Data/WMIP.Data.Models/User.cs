using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WMIP.Data.Models
{
    public class User : IdentityUser
    {
        public User()
            :base()
        {
            this.RegisteredOn = DateTime.UtcNow;
            this.Songs = new HashSet<Song>();
            this.Albums = new HashSet<Album>();
            this.Comments = new HashSet<Comment>();
            this.Articles = new HashSet<Article>();
            this.Ratings = new HashSet<Rating>();
            this.Reviews = new HashSet<Review>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
