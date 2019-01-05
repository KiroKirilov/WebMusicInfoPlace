(function () {
    $("div[data-action='down']").off("click");
    $("div[data-action='up']").off("click");

    $("div[data-action='up']").on("click", function () {
        var id = $(this).attr("data-id");
        $(`div[data-action='down'][data-id='${id}'] span`).removeClass("downvote-color");
        $(this).children("span").toggleClass("upvote-color");
        rate(id, 1);
    })

    $("div[data-action='down']").on("click", function () {
        var id = $(this).attr("data-id");
        $(`div[data-action='up'][data-id='${id}'] span`).removeClass("upvote-color");
        $(this).children("span").toggleClass("downvote-color");
        rate(id, -1);
    })

    function rate(id, ratingType) {
        var parsedData = JSON.stringify({ PostId: parseInt(id), RatingType: ratingType });
        $.ajax({
            url: "/Ratings/Rate",
            type: "POST",
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
            },
            data: parsedData,
            dataType: "json",
            contentType: "application/json",
            complete: (data) => successfullRatingCallback(data, id, ratingType),
        });
    }

    function successfullRatingCallback(data, id, ratingType) {
        if (data && data.responseJSON) {
            var newVotesCount = data.responseJSON.newScore || 0;
            $(`div[data-action='ratingsCount'][data-id='${id}']`).html(newVotesCount);
        } else {
            toastr.error("There was a problem with submitting your vote.");
        }
    }
})();