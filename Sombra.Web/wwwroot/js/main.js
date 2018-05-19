$("#header-button").click(function () {
    $(".header-dropdown-menu").css('height', '100%');
});

$("#header-close-dropdown").click(function () {
    $(".header-dropdown-menu").css('height', '0px');
});

var $carousel = $('.main-carousel').flickity({
    cellAlign: 'left',
    contain: true,
    prevNextButtons: false,
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

  // Functie change on scroll
  // Assign fixed-header
  // call scroll functie js
  // set pixel for start change
  // if statement for remove and add
  $(function() {
    var header = $(".fixed-header");
  
    $(window).scroll(function() {    
        var scroll = $(window).scrollTop();
        if (scroll >= 100) {
            header.addClass("scrolled");
        } else {
            header.removeClass("scrolled");
        }
    });
  
});