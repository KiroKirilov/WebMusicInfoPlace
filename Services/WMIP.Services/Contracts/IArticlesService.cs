using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models;
using WMIP.Services.Contracts.Common;
using WMIP.Services.Dtos.Posts;

namespace WMIP.Services.Contracts
{
    public interface IArticlesService : ICrudableEntityService<CreatePostDto, EditPostDto, Article, int>
    {
        IEnumerable<UserRatedPostDto> GetLatest(int count, string username);

        IEnumerable<UserRatedPostDto> GetAllOrderedByDate(string username);

        IQueryable<Article> GetAll();

        UserRatedPostDto GetById(int articleId, string username);
    }
}
