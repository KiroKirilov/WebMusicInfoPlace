using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;
using WMIP.Services.Dtos.Comments;

namespace WMIP.Services.Contracts
{
    public interface ICommentsService
    {
        bool Create(CreateCommentDto creationInfo, out Comment comment);

        IEnumerable<Comment> GetCommentsByUser(string userId);
    }
}
