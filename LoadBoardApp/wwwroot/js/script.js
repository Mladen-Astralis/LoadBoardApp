$(document).ready(function () {

    $(document).on("click", ".pagination a", function (e) {
        e.preventDefault();
        var params = $(this).attr("href");
        var url = '/umbraco/surface/home/getpaginatedloads';
        $.ajax({
            url: url + params,
            type: "GET",
            success: function (response) {
                $("#load-container").empty();
                $("#load-container").append(response);
            }
        });
    });

    $('body').delegate('#search-form', 'submit', function (e) {
        e.preventDefault();

        var data = $(this).serialize();
        var url = '/umbraco/surface/home/getpaginatedloads';
        $.ajax({
            url: url,
            type: 'GET',
            data: data,
            success: function (response) {
                $("#load-container").empty();
                $("#load-container").append(response);
            }
        });
    });

});