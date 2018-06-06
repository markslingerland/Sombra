

$('.category-dropdown').click(function(){
   if ($('.checkbox-dropdown').hasClass("shown")) {
    $('.checkbox-dropdown').hide();
    $('.checkbox-dropdown').removeClass("shown");
    $('.category-dropdown').removeClass("category-dropdown-clicked");

   } else {
    $('.checkbox-dropdown').show();
    $('.checkbox-dropdown').addClass("shown");
    $('.category-dropdown').addClass("category-dropdown-clicked");
   }
   
});