$(document).ready(function() {
    Search(1);
});

function Search(pageNumber = -1) {
    if (pageNumber == -1) {
        var current = $('#current-page-number');
        pageNumber = current.length ? current.data('current-pagenumber') : 1;
    }

    var url = `Charity/SearchCharities?PageNumber=${pageNumber}`;

    $('.selected-keywords .selected-keyword').each(function(index, value) {
        var keyword = $(value).text().trim();
        url += `&Keywords[${index}]=${keyword}`;
    });

    var category = 0;
    $('.checkbox-dropdown label:has(input:checked)').each(function(index, value) {
        category += $(value).data('category-id');
    });

    url += `&Category=${category}`;
    $('#search-results').load(url);
}

$('#filter').on('click', '.submit-button', function() {
    var input = $('.filter-input').val();
    if (input.length) {
        var template = $('#keyword-template').clone().html().replace('{keyword}', input);
        $('.selected-keywords').append(template);
        $('.filter-input').val('');
        ToggleNoKeywordsSelected();
        Search();
    }
});

$('.selected-keywords').on('click', '.tag-kruisje', function (event) {
    $(event.target).closest('.selected-keyword').remove();
    ToggleNoKeywordsSelected();
    Search();
});

$('.checkbox-dropdown').on('change', 'input[type="checkbox"]', function() {
    Search();
});

$('body').on('click', 'label[data-target-pagenumber]', function(event) {
    Search($(event.target).data('target-pagenumber'));
});

function ToggleNoKeywordsSelected() {
    if ($('.selected-keywords').children().length) {
        $('#keywords-placeholder').addClass('no-keywords-selected');
    } else {
        $('#keywords-placeholder').removeClass('no-keywords-selected');
    }
}

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
