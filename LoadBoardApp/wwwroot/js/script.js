$(document).on("click", ".pagination a", function (e) {
    e.preventDefault();
    const params = $(this).attr("href");
    $.ajax({
        url: '/umbraco/surface/home/getpaginatedloads' + params,
        type: "GET",
        success: function (response) {
            $("#load-container").empty();
            $("#load-container").append(response);
        }
    });
});