$('img').click(function () {
    document.getElementById('video-label').style.visibility = 'hidden';
        video = '<iframe src="' + $(this).attr('data-video') + '"></iframe>';
        $(this).replaceWith(video);
});

$(document).ready(function () {
    $('#other-story').load('Story/GetStory');
    Search(1);
});

function Search(pageNumber) {
    var url = `Story/GetStories?PageNumber=${pageNumber}`;
    if (subdomain != '') url += `&Subdomain=${subdomain}`;
    $('#more-stories').load(url);
}

$('#more-stories').on('click', 'label[data-target-pagenumber]', function (event) {
    Search($(event.target).data('target-pagenumber'));
});