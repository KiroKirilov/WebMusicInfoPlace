(function () {
    $(document).ready(() => {
        $("a.actionButton").on("click", (event) => {
            const currentButton = $(event.currentTarget);
            const itemId = currentButton.attr("data-id");
            if (itemId) {
                const newStatus = currentButton.attr("data-action");
                if (newStatus) {
                    const itemType = currentButton.attr("data-type");
                    if (itemType) {
                        var data = { ItemId: itemId, ItemType: itemType, NewApprovalStatus: newStatus };
                        makeRequest(data, itemId, itemType);
                    } else {
                        toastr.error("Couldn'get information about the item type.");
                    }
                } else {
                    toastr.error("Couldn'get information about the new status.");
                }
            } else {
                toastr.error("Couldn'get information about the item.");
            }
        })
    });

    function makeRequest(data, itemId, itemType) {
        var requestData = JSON.stringify(data);
        $.ajax({
            url: "/Admin/Approval/ChangeApprovalStatus",
            type: "POST",
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
            },
            data: requestData,
            dataType: "json",
            contentType: "application/json",
            complete: (data) => actionCompleteCallback(data, itemId, itemType),
        });
    }

    function actionCompleteCallback(data, itemId, itemType) {
        if (data && data.responseJSON && data.responseJSON.ok) {
            toastr.success("Status updated successfully.");
            $(`a[data-id=${itemId}][data-type=${itemType}]`).closest("tr").fadeOut();
            $(`a[data-id=${itemId}][data-type=${itemType}]`).closest("tr").remove();
        } else {
            if (data.responseJSON) {
                toastr.error(data.responseJSON.reason || "Something went wrong :/");
            } else {
                toastr.error("Something went wrong :/");
            }
        }
    }
})();