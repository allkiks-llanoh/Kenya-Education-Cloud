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

})