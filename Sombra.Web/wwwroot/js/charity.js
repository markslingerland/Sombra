﻿var $carousel = $('.main-carousel').flickity({
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
$('.button--previous').on('click', function () {
    $carousel.flickity('previous');
    });
// next
$('.button--next').on('click', function () {
    $carousel.flickity('next');
});
<<<<<<< HEAD
// calculate height for charity-info
var heightStoryboard = $('.main-storyboard').innerHeight();
document.getElementById('charity-info').setAttribute("style", "height:" + (heightStoryboard) + "px");
=======
>>>>>>> master