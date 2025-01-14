$(document).ready(function () {
    $('body').delegate('[data-popup-open]', "click", function (e) {
        e.preventDefault();
        var href = $(this).attr("href");
        var itemId = href.split("/");
        $.ajax({
            url: '/umbraco/surface/home/getpopupitem?loadId=' + itemId[3],
            type: "GET",
            success: function (response) {
                $("#render-popup").append(response);
                $('[data-popup="popup"]').fadeIn(350);

            }
        });
    });

    $('body').delegate('[data-popup-close]', 'click', function (e) {
        e.preventDefault();
        $('[data-popup="popup"]').fadeOut(350, function () {
            $("#render-popup").empty();
        });
    });

});