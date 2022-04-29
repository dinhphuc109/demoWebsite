// loading
setTimeout(() => (
    $('.preload').addClass('hidden')
), 2500);




//modal

$(function () {
    $('.nav-item__post').click(function () {
        $('.header__navbar-post').addClass('header__navbar-post--open');
        $('.header__overlay').addClass('header__overlay-open');
        $('body,html').addClass('modal__overFlow');
    })
    $('.close').click(function () {
        $('.header__navbar-post').removeClass('header__navbar-post--open');
        $('.header__overlay').removeClass('header__overlay-open');
        $('body,html').removeClass('modal__overFlow');
    })
    $('.hamburger-menu-button').click(function () {
        $('.header__overlay').toggleClass('header__overlay-open');
        $('body,html').toggleClass('modal__overFlow');
    })
})
//navbar open overlay


var button = document.getElementById('hamburger-menu'),
    span = button.getElementsByTagName('span')[0];

button.onclick = function () {
    span.classList.toggle('hamburger-menu-button-close');
};


$('#hamburger-menu').on('click', toggleOnClass);

function toggleOnClass(event) {
    var toggleElementId = '#' + $(this).data('toggle'),
        element = $(toggleElementId);
    element.toggleClass('on');
}

// close hamburger menu after click a
$('.menu li a').on("click", function () {
    $('#hamburger-menu').click();

});

//map
//more images
$(function () {
    $('img.img-child').hover(function () {
        let img = $(this).attr('src');
        $('img#img-main').attr('src', img);
    })
})

//map
//function initMap() {
//    const map = new google.maps.Map(document.getElementById("map"), {
//        center: { lat: 16.473041, lng: 107.421614 },
//        zoom: 5.9,
//    });
//    const deckOverlay = new deck.GoogleMapsOverlay({
//        layers: [
//            new deck.GeoJsonLayer({
//                id: "earthquakes",
//                data:
//                    "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_month.geojson",
//                filled: true,
//                pointRadiusMinPixels: 2,
//                pointRadiusMaxPixels: 200,
//                opacity: 0.4,
//                pointRadiusScale: 0.3,
//                getRadius: (f) => Math.pow(10, f.properties.mag),
//                getFillColor: [255, 70, 30, 180],
//                autoHighlight: true,
//                transitions: {
//                    getRadius: {
//                        type: "spring",
//                        stiffness: 0.1,
//                        damping: 0.15,
//                        enter: (_) => [0],
//                        duration: 10000,
//                    },
//                },
//                onDataLoad: (_) => {
//                    progress.done(); // hides progress bar
//                },
//            }),
//        ],
//    });
//    deckOverlay.setMap(map);
//}

//login-register
$(function () {
    $('.register__open').click(function () {
        $('.register__box').addClass('register__box-open');
        $('.login__box').addClass('login__box-close');
        $('.login__box').removeClass('login__box-open');
        $('.register__box').removeClass('register__box-close');
    })
    $('.login__open').click(function () {
        $('.register__box').removeClass('register__box-open');
        $('.login__box').removeClass('login__box-close');
        $('.login__box').addClass('login__box-open');
        $('.register__box').addClass('register__box-close');
    })

});