$('img').click(function () {
    document.getElementById('video-label').style.visibility = 'hidden';
    document.getElementById('video-text').style.visibility = 'hidden';
        video = '<iframe src="' + $(this).attr('data-video') + '"></iframe>';
        $(this).replaceWith(video);
});

$(document).ready(function () {
    $('#other-story').load(storyUrl);
    Search(1);
});

function Search(pageNumber) {
    var url = `${storiesUrl}?PageNumber=${pageNumber}`;
    if (subdomain != '') url += `&Subdomain=${subdomain}`;
    $('#more-stories').load(url);
}

$('#more-stories').on('click', 'label[data-target-pagenumber]', function (event) {
    Search($(event.target).data('target-pagenumber'));
});

$(document).on('click', '#scroll-to-verhalen', function () {
    ScrollTo('#more-stories');
});

$(document).on('click', '#scroll-to-deel-mijn-verhaal', function () {
    ScrollTo('.share-text');
});

function ScrollTo(selector, adjustment = 100) {
    $('html, body').animate({
            scrollTop: $(selector).offset().top - adjustment
        },
        1000);
}