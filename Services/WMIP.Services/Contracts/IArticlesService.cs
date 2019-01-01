using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Articles;

namespace WMIP.Services.Contracts
{
    public interface IArticlesService : ICrudableEntityService<CreateDto, EditPostDto, Article, int>
    {
        IEnumerable<Article> GetLatest(int count);

        IQueryable<Article> GetAll();
    }
}
