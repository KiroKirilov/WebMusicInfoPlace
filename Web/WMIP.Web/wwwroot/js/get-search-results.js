$('#searchBar').on('input', function () {
    clearTimeout(this.delay);
    this.delay = setTimeout(function () {
        $(this).trigger('search');
    }.bind(this), 500);
}).on('search', function () {
    if (this.value) {
        getResults(this.value);
    }
});

function getResults(searchTerm) {
    $.ajax({
        url: `/Search/Find?searchTerm=${searchTerm}`,
        type: "GET",
        contentType: "application/json",
        success: searchResultsCallback,
    });
}

function searchResultsCallback(data) {
    if (data) {
        $("#resultsBox").html(data);
        $("#resultsBox").slideDown();
        $(".collapse").off("click");
        $(".collapse-icon").on("click", () => {
            $("#resultsBox").slideUp();
        });
    }
}