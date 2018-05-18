$("#header-button").click(function () {
    $(".header-dropdown-menu").css('height', '100%');
});

$("#header-close-dropdown").click(function () {
    $(".header-dropdown-menu").css('height', '0px');
});

var $carousel = $('.main-carousel').flickity({
    cellAlign: 'left',
    contain: true,
    pageDots: false, 

    prevNextButtons: false,
    pageDots: false
  });

  // Flickity instance
var flkty = $carousel.data('flickity');
// elements
var $cellButtonGroup = $('.button-group--cells');
var $cellButtons = $cellButtonGroup.find('.button');

  // previous
  $('.button--previous').on( 'click', function() {
    $carousel.flickity('previous');
  });
  // next
  $('.button--next').on( 'click', function() {
    $carousel.flickity('next');
  });

