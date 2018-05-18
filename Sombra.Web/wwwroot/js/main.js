$("#header-button").click(function () {
    $(".header-dropdown-menu").css('height', '100%');
});

$("#header-close-dropdown").click(function () {
    $(".header-dropdown-menu").css('height', '0px');
});

$('.main-carousel').flickity({
    // options
    cellAlign: 'left',
    contain: true,
    pageDots: false, 
  });