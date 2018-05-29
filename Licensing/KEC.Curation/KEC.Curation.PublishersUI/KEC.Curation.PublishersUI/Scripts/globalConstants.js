const apiBaseUrl = "https://curationapi.kec.ac.ke/api";
var currentUserGuid;
$(document).ready(function () {

    $('.summernote').summernote();
    fetchUserHeader((userGuid) => {
        currentUserGuid = userGuid;
    });

});

function fetchUserHeader(callback) {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            //
            // The following headers may often be similar
            // to those of the original page request...
            //
            if (callback && typeof callback === 'function') {
                callback(request.getResponseHeader('x-user-guid'));
            }
        }
    };

    //
    // Re-request the same page (document.location)
    // We hope to get the same or similar response headers to those which 
    // came with the current page, but we have no guarantee.
    // Since we are only after the headers, a HEAD request may be sufficient.
    //
    request.open('HEAD', document.location, true);
    request.send(null);
}

function ShowAlert(message, type) {

    if (type === "error") {
        $.notify(message, 'error');
    } else if (type === "warning") {
        $.notify(message, 'warning');
    } else if (type === "success") {
        $.notify(message, 'success');

    } else if (type === "info") {
        $.notify(message, 'info');
    }
}
function hookBtnSelect() {
    $(document).on('click', '.btn-select', function (e) {
        e.preventDefault();
        var ul = $(this).find("ul");
        if ($(this).hasClass("active")) {
            if (ul.find("li").is(e.target)) {
                var target = $(e.target);
                target.addClass("selected").siblings().removeClass("selected");
                var value = target.html();
                $(this).find(".btn-select-input").val(value);
                $(this).find(".btn-select-value").html(value);
            }
            ul.hide();
            $(this).removeClass("active");
        }
        else {
            $('.btn-select').not(this).each(function () {
                $(this).removeClass("active").find("ul").hide();
            });
            ul.slideDown(300);
            $(this).addClass("active");
        }
    });

    $(document).on('click', function (e) {
        var target = $(e.target).closest(".btn-select");
        if (!target.length) {
            $(".btn-select").removeClass("active").find("ul").hide();
        }
    });
}
function convertUTCDateToLocalDate(date) {
    var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;
}
