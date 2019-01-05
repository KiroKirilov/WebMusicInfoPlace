(function () {
    [].forEach.call(document.getElementsByClassName('mvc-grid'), function (element) {
        new MvcGrid(element);
    });
})();