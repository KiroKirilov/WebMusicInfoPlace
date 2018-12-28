using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Data.Models;

namespace WMIP.Services.Contracts
{
    public interface ICommentsService
    {
        bool CreateNew(string title, string body, string userId, int postId, out Comment comment);
    }
}
