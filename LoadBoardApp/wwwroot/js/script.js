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


    $('#search-form').on('submit', function (e) {
        e.preventDefault();

        var url = '/umbraco/surface/home/searchloads';

        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                
            },
            error: function () {
                
            }
        });
    });

});