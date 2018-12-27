using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data.Models.Common;
using WMIP.Data.Models.Enums;

namespace WMIP.Services.Contracts
{
    public interface IApprovalService
    {
        IEnumerable<BaseMusicModel> GetAllItemsForApproval();

        IEnumerable<BaseMusicModel> GetUsersApprovalRequests(string userId);

        bool ChangeApprovalStatus(int itemId, ActionItemType itemType, ApprovalStatus newStatus);
    }
}
