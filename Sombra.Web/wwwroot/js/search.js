$(document).ready(function() {
    Search(1);
});

function Search(pageNumber = -1) {
    if (pageNumber == -1) {
        var current = $('#current-page-number');
        pageNumber = current.length ? current.data('current-pagenumber') : 1;
    }

    var url = `Charity/SearchCharities?PageNumber=${pageNumber}`;

    $('.selected-keywords .selected-keyword:not([data-category])').each(function(index, value) {
        var keyword = $(value).text().trim();
        url += `&Keywords[${index}]=${encodeURIComponent(keyword)}`;
    });

    var category = 0;
    $('.checkbox-dropdown label:has(input:checked)').each(function(index, value) {
        category += $(value).data('category-id');
    });

    url += `&Category=${category}`;
    $('#search-results').load(url);
}

$('#filter').on('click', '.submit-button', KeywordEntered);
$('.filter-input').on('keyup', function(event) {
    if (event.keyCode == 13) KeywordEntered();
});

function KeywordEntered() {
    var input = $('.filter-input').val();
    if (input.length) {
        var template = $('#keyword-template').clone().html().replace('{keyword}', input);
        $('.selected-keywords').append(template);
        $('.filter-input').val('');
        ToggleNoKeywordsSelected();
        Search();
    }
}

$('.selected-keywords').on('click', '.tag-kruisje', function (event) {
    var keyword = $(event.target).closest('.selected-keyword');
    if (keyword.attr('data-category')) {
        var category = keyword.attr('data-category');
        $('.checkbox-dropdown').find(`label[data-category-id="${category}"] input[type="checkbox"]`).prop('checked', false);
    }
    keyword.remove();
    ToggleNoKeywordsSelected();
    Search();
});

$('.checkbox-dropdown').on('change', 'input[type="checkbox"]', function (event) {
    var target = $(event.target);
    var label = target.closest('label');
    var categoryId = label.data('category-id');
    var category = label.text();
    if (target.is(':checked')) {
        var template = $('#keyword-template').clone().html().replace('{keyword}', category);
        var templateElement = $(template).attr('data-category', categoryId);
        $('.selected-keywords').append(templateElement);
    } else {
        $(`.selected-keywords .selected-keyword[data-category="${categoryId}"]`).remove();
    }
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