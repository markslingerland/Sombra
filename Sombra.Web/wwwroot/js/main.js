$("#header-button").click(function () {
    $(".header-dropdown-menu").css('height', '100%');
});

$("#header-close-dropdown").click(function () {
    $(".header-dropdown-menu").css('height', '0px');
});

  // Functie change on scroll
  // Assign fixed-header
  // call scroll functie js
  // set pixel for start change
  // if statement for remove and add
  $(function() {
    var header = $("#header");
  
    $(window).scroll(function() {    
        var scroll = $(window).scrollTop();
        if (scroll >= 1) {
            header.addClass("scrolled");
        } else {
            header.removeClass("scrolled");
        }
    });
  
});

