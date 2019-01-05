var selectedButton = $("");

$(".newCommentButton").off("click");
$(".newCommentButton").on("click", (event) => {
    $(event.currentTarget).siblings(".newCommentForm").slideToggle();
    selectedButton = $(event.currentTarget);
})

function onCommentComplete(data) {
    if (data && data.responseJSON && data.responseJSON.ok) {
        toastr.success("Comment added successfully.");
        $(".newCommentForm").slideUp();
        var newCommentInfo = data.responseJSON.info;
        var newCommentHtml = buildNewComment(newCommentInfo.title, newCommentInfo.body, newCommentInfo.author, newCommentInfo.date, newCommentInfo.time);
        var newReplyContainer = selectedButton.parent().parent().parent().parent().children(".commentBody").children(".newCommentsContainer");
        var newBaseCommentContainer = selectedButton.parent().children(".newCommentsContainer");
        if (newReplyContainer.length > 0) {
            newReplyContainer.prepend(newCommentHtml);
        } else if (newBaseCommentContainer.length > 0) {
            newBaseCommentContainer.prepend(newCommentHtml);
        }
    }
}

function onCommentFail(data) {
    toastr.error("Something went wrong :/");
}