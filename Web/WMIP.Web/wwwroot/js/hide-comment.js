$(".hideComment").off("click");
$(".hideComment").on("click", handleCommentHide);

function handleCommentHide(event) {
    var currentTargetObj = $(event.currentTarget);
    currentTargetObj.parent().parent().parent().children(".commentBody").first().slideToggle();
    var currentSymbol = currentTargetObj.text();
    if (currentSymbol === "-") {
        currentTargetObj.text("+");
    } else {
        currentTargetObj.text("-");
    }
}