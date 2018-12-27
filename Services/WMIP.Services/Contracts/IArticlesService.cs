using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts
{
    public interface IArticlesService
    {
        bool CreateNew(string title, string body, string userId);
    }
}
