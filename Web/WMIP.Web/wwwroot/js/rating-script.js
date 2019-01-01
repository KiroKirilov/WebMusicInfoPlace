$("div[data-action='down']").off("click");
$("div[data-action='up']").off("click");

$("div[data-action='up']").on("click", function () {
    var id = $(this).attr("data-id");
    rate(id, 1);
})

$("div[data-action='down']").on("click", function () {
    var id = $(this).attr("data-id");
    rate(id, -1);
})

function rate(id, ratingType) {
    var parsedData = JSON.stringify({ PostId: parseInt(id), RatingType: ratingType });
    $.ajax({
        url: "/Ratings/Rate",
        type: "POST",
        data: parsedData,
        dataType: "json",
        contentType: "application/json",
        complete: (data) => successfullRatingCallback(data, id),
    });
}

function successfullRatingCallback(data, id) {
    if (data && data.responseJSON) {
        var newVotesCount = data.responseJSON.newScore || 0;
        $(`div[data-action='ratingsCount'][data-id='${id}']`).html(newVotesCount);
    } else {
        toastr.error("There was a problem with submitting your vote.");
    }
}