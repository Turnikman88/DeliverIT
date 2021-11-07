$(function () {
    $("#tabs").tabs();

    if ($('.owl-trusted').length) {
        $('.owl-trusted').owlCarousel({
            loop: true,
            nav: false,
            dots: true,
            items: 4,
            margin: 30,
            autoplay: false,
            smartSpeed: 700,
            autoplayTimeout: 6000,
            responsive: {
                0: {
                    items: 1,
                    margin: 0
                },
                460: {
                    items: 2,
                    margin: 0
                },
                576: {
                    items: 3,
                    margin: 20
                },
                992: {
                    items: 4,
                    margin: 30
                }
            }
        });
    }
    if ($('.owl-testimonials').length) {
        $('.owl-testimonials').owlCarousel({
            loop: true,
            nav: false,
            dots: true,
            items: 1,
            margin: 30,
            autoplay: false,
            smartSpeed: 700,
            autoplayTimeout: 6000,
            responsive: {
                0: {
                    items: 1,
                    margin: 0
                },
                460: {
                    items: 1,
                    margin: 0
                },
                576: {
                    items: 1,
                    margin: 20
                },
                992: {
                    items: 1,
                    margin: 30
                }
            }
        });
    }
});

// Page loading animation
$(window).on('load', function () {
    if ($('.cover').length) {
        $('.cover').parallax({
            imageSrc: $('.cover').data('image'),
            zIndex: '1'
        });
    }

    $("#preloader").animate({
        'opacity': '0'
    }, 600, function () {
        setTimeout(function () {
            $("#preloader").css("visibility", "hidden").fadeOut();
        }, 300);
    });
});

$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    var header = $('header').height();

    if (scroll >= header) {
        $("header").addClass("background-header");
    } else {
        $("header").removeClass("background-header");
    }
});

$(window).on('load', function () {
    if (window.location.pathname == "/" || window.location.pathname.toLowerCase() == "/home/index") {
        $("header").addClass("main-header");
    } else {
        $("header").removeClass("main-header");
    }
});

function navActiveChange(elId) {
    var el = document.getElementsByClassName("active")[0];
    el.classList.remove("active");
    var nel = document.getElementById(elId);
    nel.classList.add("active");
}

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal .show').remove();
                    $('#form-modal').css('display', 'none');
                    $('.modal-backdrop').remove();
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            }

        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

ExecuteEmptyAjax = (url) => {
    $.ajax({
        type: "POST",
        url: url,
        success: function (res) {
            $('#view-all').html(res.html)
        }
    })
}

$(".form-search").change(function () {
    $(this).attr('action', $('#sel').val());
});

SearchBarAjax = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                }
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
    }
}

