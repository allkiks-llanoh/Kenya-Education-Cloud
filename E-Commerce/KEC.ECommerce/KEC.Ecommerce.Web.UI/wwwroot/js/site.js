/////////////////////////////////Public Functions and variables /////////////////////////////////////////////////////
var notify = function feedbackInfo(message, msgClass, title) {
    let notify = Metro.notify;
    notify.setup({
        duration: 2000,
        animation: 'easeOutBounce'
    });
    notify.create(message, title, { cls: msgClass });
    notify.reset();
};
var showValidationErrorInfoBox = function (data) {
    let elements = $.parseHTML(data);
    let found = $('.validation-summary-errors', $(elements));
    if (found !== null && found.children().length > 0) {
        Metro.infobox.create(found, "alert");
    }
};
var notifySuccess = function (data) {
    var elements = $.parseHTML(data);
    var found = $('.validation-summary-errors', $(elements));
    if (found !== null && found.children().length > 0) {
        Metro.infobox.create(found, "alert");
    } else {
        notify("Item(s) added to cart successfully", "success", "Cart");
    }
};
var notifyFail = function (jXHR) {
    notify("Item(s) could not be added to cart", "alert", "Cart");
};

function getAllUrlParams(url) {

    // get query string from url (optional) or window
    let queryString = url ? url.split('?')[1] : window.location.search.slice(1);

    // we'll store the parameters here
    let obj = {};

    // if query string exists
    if (queryString) {

        // stuff after # is not part of query string, so get rid of it
        queryString = queryString.split('#')[0];

        // split our query string into its component parts
        let arr = queryString.split('&');

        for (let i = 0; i < arr.length; i++) {
            // separate the keys and the values
            let a = arr[i].split('=');

            // in case params look like: list[]=thing1&list[]=thing2
            let paramNum = undefined;
            let paramName = a[0].replace(/\[\d*\]/, function (v) {
                paramNum = v.slice(1, -1);
                return '';
            });

            // set parameter value (use 'true' if empty)
            let paramValue = typeof a[1] === 'undefined' ? true : a[1];

            // (optional) keep case consistent
            paramName = paramName.toLowerCase();
            paramValue = paramValue.toLowerCase();

            // if parameter name already exists
            if (obj[paramName]) {
                // convert value to array (if still string)
                if (typeof obj[paramName] === 'string') {
                    obj[paramName] = [obj[paramName]];
                }
                // if no array index number specified...
                if (typeof paramNum === 'undefined') {
                    // put the value on the end of the array
                    obj[paramName].push(paramValue);
                }
                // if array index number specified...
                else {
                    // put the value at that index number
                    obj[paramName][paramNum] = paramValue;
                }
            }
            // if param name doesn't exist yet, set it
            else {
                obj[paramName] = paramValue;
            }
        }
    }

    return obj;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    $(window).resize(function () {
        $('body').css('padding-top', parseInt($('#main-appbar').css("height")));
    });
    $('body').css('padding-top', parseInt($('#main-appbar').css("height")));
    $('body').append('<div id="toTop" class="button info"><span class="mif-arrow-up icon fg-white"></span> Back to Top</div>');
    $(window).scroll(function () {
        if ($(this).scrollTop() !== 0) {
            $('#toTop').fadeIn();
        } else {
            $('#toTop').fadeOut();
        }
    });
    var html_content =
        "<h3>What is Lorem Ipsum?</h3>" +
        "<p>Lorem Ipsum is simply dummy text...</p>";
    $('#pagination').pagination({
        pages: $('#hdnTotalNumberOfPages').val(),
        currentPage: $('#hdnCurrentPage').val(),
        itemsOnPage: 1,
        cssStyle: 'light-theme',
        onPageClick: function (pageNo, event) {
            event.preventDefault();
            let currentUrl = window.location.href;
            if (typeof getAllUrlParams(currentUrl).pagenumber !== 'undefined') {
                let currentPage = getAllUrlParams(currentUrl).pagenumber;
                currentUrl = currentUrl.replace(`pageNumber=${currentPage}`, `pageNumber=${pageNo}`);
            } else {
                if (currentUrl.includes('&')) {
                    currentUrl = `${currentUrl}&pageNumber=${pageNo}`;
                } else {
                    currentUrl = `${currentUrl}?pageNumber=${pageNo}`;
                }

            }
            window.location.href = currentUrl;
        },
        hrefTextSuffix: '',
        selectOnClick: true
    });

    $('#toTop').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
    $('.match-height').each(function () {
        $(this).find('.card').not('.card .card').matchHeight();
    });

    $('.owl-carousel').owlCarousel({
        loop: true,
        autoplay: true,
        autoplayHoverPause: true,
        animateOut: 'slideOutDown',
        animateIn: 'flipInX',
        margin: 10,
        smartSpeed: 250,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            600: {
                items: 2,
                nav: true
            },
            1000: {
                items: 3,
                nav: true
            }
        }
    });
    $('.loading').hide();
    let found = $('.validation-summary-errors');
    if (found !== null && found.children().length > 0) {
        Metro.infobox.create(found, "alert");
    }
    $('.logout').on('click', function (e) {
        e.preventDefault();
        $('#logout-form').submit();
    });

});