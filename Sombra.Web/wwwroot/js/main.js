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
    var header = $("#header .fixed-header");
  
    $(window).scroll(function() {    
        var scroll = $(window).scrollTop();
        if (scroll >= 1) {
            header.addClass("scrolled");
        } else {
            header.removeClass("scrolled");
        }
    });
  
});

var disclaimerCookieName = 'ikdoneernu-disclaimer';
$(document).ready(function () {
    var disclaimerCookie = localStorage.getItem(disclaimerCookieName);
    if (disclaimerCookie != null) {
        var now = new Date().getTime();
        if (now - disclaimerCookie > 1209600)
            $('#disclaimer-modal').modal('show');
    } else {
        $('#disclaimer-modal').modal('show');
    }
});

$('#close-disclaimer-modal').on('click', function() {
    localStorage.setItem(disclaimerCookieName, new Date().getTime());
});