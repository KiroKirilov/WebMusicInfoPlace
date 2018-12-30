using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models;

namespace WMIP.Services.Contracts
{
    public interface IArticlesService
    {
        bool CreateNew(string title, string body, string summary, string userId);

        bool Edit(int articleId, string title, string body, string summary);

        bool Delete(int articleId);

        IEnumerable<Article> GetLatest(int count);

        Article GetById(int articleId);

        IQueryable<Article> GetAll();
    }
}
