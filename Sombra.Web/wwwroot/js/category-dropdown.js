$('.category-dropdown').click(function () {
    if ($('.checkbox-dropdown').hasClass("shown")) {
        CloseCategoryDropdown();
    } else {
        OpenCategoryDropdown();
    }
});

function CloseCategoryDropdown() {
    $('.checkbox-dropdown').hide();
    $('.checkbox-dropdown').removeClass("shown");
    $('.category-dropdown').removeClass("category-dropdown-clicked");
}

function OpenCategoryDropdown() {
    $('.checkbox-dropdown').show();
    $('.checkbox-dropdown').addClass("shown");
    $('.category-dropdown').addClass("category-dropdown-clicked");
}

$(document).click(function(event) {
    if (!$(event.target).closest('.category-dropdown-holder').length)
        CloseCategoryDropdown();
})