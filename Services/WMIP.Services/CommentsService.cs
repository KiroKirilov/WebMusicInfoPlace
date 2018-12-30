using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;

namespace WMIP.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly WmipDbContext context;

        public CommentsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool CreateNew(string title, string body, string userId, int postId, out Comment comment)
        {
            try
            {
                comment = new Comment
                {
                    Title = title,
                    Body = body,
                    UserId = userId,
                    CommentedOnId = postId,                     
                };

                this.context.Comments.Add(comment);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                comment = null;
                return false;
            }
        }

        public IEnumerable<Comment> GetCommentsByUser(string userId)
        {
            try
            {
                return this.context.Comments.Where(c => c.UserId == userId);
            }
            catch
            {
                return new List<Comment>();
            }
        }
    }
}
