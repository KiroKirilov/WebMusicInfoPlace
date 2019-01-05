using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Comments;

namespace WMIP.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly WmipDbContext context;

        public CommentsService(WmipDbContext context)
        {
            this.context = context;
        }

        public bool Create(CreateCommentDto creationInfo, out Comment comment)
        {
            try
            {
                comment = new Comment
                {
                    Title = creationInfo.Title,
                    Body = creationInfo.Body,
                    UserId = creationInfo.UserId,
                    CommentedOnId = creationInfo.PostId,                     
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
