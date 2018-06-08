var $carousel = $('.main-carousel').flickity({
<<<<<<< HEAD
    cellAlign: 'left',
    contain: true,
    prevNextButtons: false,
    pageDots: false
});
=======
 cellAlign: 'left',
        contain: true,
    prevNextButtons: false,
    pageDots: false
      });
>>>>>>> master

// Flickity instance
var flkty = $carousel.data('flickity');
// elements
var $cellButtonGroup = $('.button-group--cells');
var $cellButtons = $cellButtonGroup.find('.button');