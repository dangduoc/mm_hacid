$(function () {
   
    $(window).on('load', function () {
        if ($('#nav_theme')) {
            if ($('#nav_theme').val() == 1) {
                $('nav.menu').addClass('dark');
            }
        }
    });
    $('body').click(function (event) {
        console.log(event);
        if (!$(event.target).closest('.sub-menu').length && !$(event.target).closest('.open_sub_menu').length) {
            $('nav#menu').removeClass('open');
        }
    });
    $('.open_sub_menu').on('click', function () {
        $(this).closest('.menu').toggleClass('open');
    });

    $('.top-filter-list button').on('click', function () {
        $(this).closest('.top-filter-list').toggleClass('open');
    });

    navBarControl();
    $(window).on('scroll', function () {
        $('nav#menu').removeClass('open');
        navBarControl();
    });
    var previousScroll = 0;
    function navBarControl() {
        if ($('#menu').hasClass('open')) return;
        var nav_height = $('#menu').outerHeight();
        var currentScroll = $(this).scrollTop();

        if (currentScroll > 0 && currentScroll < $(document).height() - $(window).height()) {

            if (currentScroll > previousScroll) {
                window.setTimeout(function () {
                    $('#menu').css('top', -nav_height);
                }, 100);

            } else {
                window.setTimeout(function () {
                    $('#menu').css('top', '0px');
                    if ($(this).scrollTop() > 0) {
                        $('#menu').addClass('fixed');
                    }
                    else {
                        $('#menu').removeAttr('style');
                        $('#menu').removeClass('fixed');
                    }
                }, 100);
            }
            /* 
              Set the previous scroll value equal to the current scroll.
            */
            previousScroll = currentScroll;
        }

    }


    $('form.select_language a.lang').on('click', function (e) {
        e.preventDefault();
        let selected_lang = $(this).attr('data-id');
        console.log(selected_lang);
        $(this).closest('form').find('select').val(selected_lang).change();
    });
})
let vh = window.innerHeight * 0.01;
document.documentElement.style.setProperty('--vh', `${vh}px`);
window.addEventListener('resize', () => {
    // We execute the same script as before
    let vh = window.innerHeight * 0.01;
    document.documentElement.style.setProperty('--vh', `${vh}px`);
});

