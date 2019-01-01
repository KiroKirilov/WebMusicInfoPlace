$("a.changeRoleBtn").on("click", (event) => {
    const currentButton = $(event.currentTarget);
    const userId = currentButton.attr("data-id");
    if (userId) {
        const newRole = $(`select[data-id=${userId}]`).val().toString();
        const oldRole = $(`p[data-id=${userId}]`).text().toString();
        if (true) {
            if (newRole != oldRole && !oldRole.includes(newRole)) {
                const data = JSON.stringify({ NewRole: newRole, UserId: userId });
                $.ajax({
                    url: "/Admin/Users/ChangeRole",
                    type: "POST",
                    data: data,
                    dataType: "json",
                    contentType: "application/json",
                    complete: (data) => changeRoleCallback(data, userId, newRole),
                });
            } else {
                toastr.warning('User is already in that role.');
            }
        } else {
            toastr.error('Please select a role first.');
        }
    } else {
        toastr.error("Couldn'get information about the user.");
    }
})

function changeRoleCallback(data, userId, newRole) {
    if (data && data.responseJSON && data.responseJSON.ok) {
        toastr.success("Role changed successfully");
        $(`select[data-id=${userId}]`).val(newRole);
        $(`p[data-id=${userId}]`).text(newRole);
    } else {
        if (data.responseJSON) {
            toastr.error(data.responseJSON.reason || "Something went wrong :/");
        } else {
            toastr.error("Something went wrong :/");
        }
    }
}