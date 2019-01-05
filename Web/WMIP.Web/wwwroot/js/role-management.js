function changeRoleCallback(data) {
    if (data && data.responseJSON && data.responseJSON.ok) {
        toastr.success("Role changed successfully");
        $(`select[data-id=${data.responseJSON.userId}]`).val(data.responseJSON.newRole);
        $(`p[data-id=${data.responseJSON.userId}]`).text(data.responseJSON.newRole);
    } else {
        if (data.responseJSON) {
            toastr.error(data.responseJSON.reason || "Something went wrong :/");
        } else {
            toastr.error("Something went wrong :/");
        }
    }
}

function changeRoleFail() {
    toastr.error("Something went wrong :/");
}