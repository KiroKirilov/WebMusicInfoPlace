using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface IUserCreatedEntityService
    {
        bool IsUserCreatorById(string userId, int id);

        bool IsUserCreatorByName(string username, int id);
    }
}
