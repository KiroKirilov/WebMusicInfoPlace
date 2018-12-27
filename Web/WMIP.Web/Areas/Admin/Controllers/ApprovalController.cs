using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;
using WMIP.Services.Contracts;
using WMIP.Web.Areas.Admin.Models.Approval;

namespace WMIP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ApprovalController : Controller
    {
        private readonly IApprovalService approvalService;
        private readonly IMapper mapper;

        public ApprovalController(IApprovalService approvalService, IMapper mapper)
        {
            this.approvalService = approvalService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var allItemsForApproval = this.approvalService.GetAllItemsForApproval();
            var mappedItems = this.mapper.Map<ApprovalItemViewModel[]>(allItemsForApproval);

            return this.View(mappedItems);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ChangeApprovalStatus([FromBody]BasicApprovalItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { ok = false, reason = GenericMessages.InvalidDataProvided });
            }

            int parsedId = 0;

            try
            {
                parsedId = int.Parse(model.ItemId);
            }
            catch
            {
                return this.Json(new { ok = false, reason = GenericMessages.InvalidDataProvided });
            }

            var successfullyChangesStatus = this.approvalService.ChangeApprovalStatus(parsedId, model.ItemType, model.NewApprovalStatus);
            if (!successfullyChangesStatus)
            {
                return this.Json(new { ok = false, reason = string.Format(GenericMessages.CouldntDoSomething, "change status") });
            }

            return this.Json(new { ok = true });
        }
    }
}
